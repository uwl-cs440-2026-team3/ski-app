import com.sun.net.httpserver.*;
import java.io.*;
import java.lang.reflect.InvocationTargetException;
import java.nio.charset.StandardCharsets;
import java.security.*;
import java.sql.*;
import java.util.Base64;
import java.util.concurrent.*;

import com.fasterxml.jackson.core.*;
import com.fasterxml.jackson.databind.ObjectMapper;

public class RouteContext {
    private final static ObjectMapper JSONMapper = new ObjectMapper();
    private static ConcurrentHashMap<String, String> sessions = new
        ConcurrentHashMap<>();

    // Shared by both skier and coach registration.
    private record RegisterRequest(String email, String name, String password) {};
    private record LoginRequest(String email, String password) {};
    private record TeamCreateRequest(String name) {};
    private record CourseCreateRequest(String name) {};

    public static void registerRoutes(HttpsServer server) {
        server.createContext("/register",
                             (HttpExchange hx) -> new RegistrationHandler(hx).handle());
        server.createContext("/login",
                             (HttpExchange hx) -> new LoginHandler(hx).handle());
        server.createContext("/team",
                             (HttpExchange hx) -> new TeamCreateHandler(hx).handle());
        server.createContext("/course",
                             (HttpExchange hx) -> new CourseCreateHandler(hx).handle());
        server.createContext("/registercoach",
                             (HttpExchange hx) -> new CoachRegistrationHandler(hx).handle());
    }


    private static abstract class RequestLifecycle<E extends Record> {
        protected HttpExchange hx;
        private Class<E> recordType;

        public RequestLifecycle(HttpExchange hx, Class<E> type) {
            this.hx = hx;
            this.recordType = type;
        }

        public void handle() throws IOException {
            try {
                String method = this.hx.getRequestMethod();

                if ("POST".equals(method)) {
                    // Continue
                } else if (RequestLifecycle.isRecognizedHttpMethod(method)) {
                    this.methodNotAllowed("POST");
                    return;
                } else {
                    this.notImplemented();
                    return;
                }

                if (!this.isAuthorized()) {
                    return;
                }

                E req;
                try {
                    req = JSONMapper.readValue(this.hx.getRequestBody(), this.recordType);
                } catch (JacksonException je) {
                    this.badRequest("Invalid JSON");
                    return;
                }

                if (hasBlankField(req)) {
                    this.badRequest("Missing or empty JSON fields.");
                    return;
                }

                this.handleDetail(req);
            } finally {
                this.hx.close();
            }
        }

        abstract boolean isAuthorized() throws IOException;
        abstract void handleDetail(E req) throws IOException;

        protected static boolean isRecognizedHttpMethod(String m) {
            return "GET".equals(m) || "HEAD".equals(m) || "POST".equals(m) ||
                   "PUT".equals(m) || "DELETE".equals(m) || "OPTIONS".equals(m) ||
                   "PATCH".equals(m) || "TRACE".equals(m) || "CONNECT".equals(m);
        }

        protected void sendText(int status, String body) throws IOException {
            byte[] bytes = body.getBytes(StandardCharsets.UTF_8);
            this.hx.getResponseHeaders().set("Content-Type", "text/plain; charset=utf-8");
            this.hx.sendResponseHeaders(status, bytes.length);
            try (OutputStream os = this.hx.getResponseBody()) {
                os.write(bytes);
            }
        }

        protected void badRequest(String msg) throws IOException {
            this.sendText(400, msg);
        }

        protected void conflict(String msg) throws IOException {
            this.sendText(409, msg);
        }

        private void methodNotAllowed(String allow) throws IOException {
            this.hx.getResponseHeaders().set("Allow", allow);
            this.hx.sendResponseHeaders(405, -1);
        }

        private void notImplemented() throws IOException {
            this.hx.sendResponseHeaders(501, -1);
        }
    }

    private static abstract class UnprivilegedHandler<E extends Record> extends
        RequestLifecycle<E> {

        public UnprivilegedHandler(HttpExchange hx, Class<E> type) {
            super(hx, type);
        }

        @Override
        boolean isAuthorized() throws IOException {
            return true;
        }
    }

    private static abstract class PrivilegedHandler<E extends Record>
        extends RequestLifecycle<E> {

        PrivilegedHandler(HttpExchange hx, Class<E> type) {
            super(hx, type);
        }

        @Override
        public boolean isAuthorized() throws IOException {
            return null != this.requireAdmin();
        }

        private String requireAdmin() throws IOException {
            // Get the token first
            String auth = this.hx.getRequestHeaders().getFirst("Authorization");
            if (auth == null || !auth.startsWith("Bearer ")) {
                this.sendText(403, "Missing token");
                return null;
            }

            String token = auth.substring("Bearer ".length()).trim();

            // Token -> role
            String role = RouteContext.sessions.get(token);
            if (role == null || !role.equals("admin")) {
                this.sendText(403, "Not authorized");
                return null;
            }

            return role;
        }
    }

    private static class RegistrationHandler extends
        UnprivilegedHandler<RegisterRequest> {
        public RegistrationHandler(HttpExchange hx) {
            super(hx, RegisterRequest.class);
        }

        @Override
        void handleDetail(RegisterRequest req) throws IOException {

            String hash;
            try {
                hash = AuthUtil.hashPassword(req.password);
            } catch (NoSuchAlgorithmException e) {
                this.sendText(500, "Server misconfigured");
                return;
            }

            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql =
                    "INSERT INTO users (email, name, pwhash, role_mask) VALUES (?, ?, ?, ?)";
                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, req.email);
                    ps.setString(2, req.name);
                    ps.setString(3, hash);
                    ps.setInt(4, 0);
                    ps.executeUpdate();
                }
            } catch (SQLException se) {
                // If the UNIQUE constraint (email) has failed, 409 makes sense
                this.conflict("Email already registered");
                return;
            }

            this.sendText(201, "registered");
        }
    }

    private static class TeamCreateHandler extends
        PrivilegedHandler<TeamCreateRequest> {
        public TeamCreateHandler(HttpExchange hx) {
            super(hx, TeamCreateRequest.class);
        }

        @Override
        void handleDetail(TeamCreateRequest req) throws IOException {
            // DB insert
            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql = "INSERT INTO teams (name) VALUES (?)";
                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, req.name);
                    ps.executeUpdate();
                }
            } catch (SQLException se) {
                if (se.getMessage().contains("UNIQUE")) {
                    this.conflict("Team already exists");
                } else {
                    this.sendText(500, se.getMessage());
                }
                return;
            }

            this.sendText(201, "Team created");
        }
    }

    private static class CourseCreateHandler extends
        PrivilegedHandler<CourseCreateRequest> {
        public CourseCreateHandler(HttpExchange hx) {
            super(hx, CourseCreateRequest.class);
        }

        @Override
        void handleDetail(CourseCreateRequest req) throws IOException {
            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql = "INSERT INTO courses (name) VALUES (?)";

                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, req.name);
                    ps.executeUpdate();
                }
            } catch (SQLException se) {
                if (se.getMessage().contains("UNIQUE")) {
                    this.conflict("Course already exists");
                } else {
                    this.sendText(500, se.getMessage());
                }

                return;
            }

            this.sendText(201, "Course created");
        }
    }

    private static class LoginHandler extends UnprivilegedHandler<LoginRequest> {
        public LoginHandler(HttpExchange hx) {
            super(hx, LoginRequest.class);
        }

        @Override
        void handleDetail(LoginRequest req) throws IOException {
            try {
                String role = this.authenticate(req);
                if (!role.equals("noauth")) {
                    this.sendLoginSuccess(role);
                } else {
                    this.sendLoginFailure();
                }
            } catch (SQLException e) {
                e.printStackTrace(System.err);
                this.sendText(500, "");
            } catch (NoSuchAlgorithmException e) {
                this.sendText(500, "Server misconfigured");
            }
        }

        // Returns the role; the special "noauth" value indicates an authentication failure.
        private String authenticate(
            LoginRequest login) throws SQLException, IOException,
            NoSuchAlgorithmException {
            String sql = "SELECT pwhash, role_mask FROM users WHERE email = ?";
            try (Connection conn = DriverManager.getConnection(Config.databaseURL);
                        PreparedStatement ps = conn.prepareStatement(sql)) {
                ps.setString(1, login.email);
                ResultSet result = ps.executeQuery();

                if (!result.next()) {
                    return "noauth";
                }

                // There can only be zero or one result rows, since emails are
                // constrained to be unique.
                if (result.getString("pwhash").equals(AuthUtil.hashPassword(
                        login.password))) {
                    return this.getRole(result.getInt("role_mask"));
                } else {
                    return "noauth";
                }
            }
        }

        private String getRole(int roleMask) {
            switch(roleMask) {
            case 2:
                return "coach";
            case 1:
                return "admin";
            case 0:
                return "skier";
            default:
                return "noauth";
            }
        }

        private void sendLoginSuccess(String role) throws IOException,
            JsonProcessingException {
            String token = this.genToken();
            RouteContext.sessions.put(token, role);

            LoginResponse responseData = new LoginResponse(token, role);
            String response = RouteContext.JSONMapper.writeValueAsString(responseData);
            this.sendText(200, response);
        }

        private String genToken() {
            byte[] token = new byte[256];
            new SecureRandom().nextBytes(token);
            return Base64.getUrlEncoder()
                   .withoutPadding()
                   .encodeToString(token);
        }

        private void sendLoginFailure() throws IOException {
            this.sendText(403, "Unauthorized Credentials");
        }

        private record LoginResponse(String token, String role) {};
    }


    private static class CoachRegistrationHandler extends
        PrivilegedHandler<RegisterRequest> {
        public CoachRegistrationHandler(HttpExchange hx) {
            super(hx, RegisterRequest.class);
        }

        @Override
        void handleDetail(RegisterRequest req) throws IOException {
            String hash;
            try {
                hash = AuthUtil.hashPassword(req.password);
            } catch (NoSuchAlgorithmException e) {
                this.sendText(500, "Server misconfigured");
                return;
            }

            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql =
                    "INSERT INTO users (email, name, pwhash, role_mask) VALUES (?, ?, ?, ?)";
                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, req.email);
                    ps.setString(2, req.name);
                    ps.setString(3, hash);
                    ps.setInt(4, 2);
                    ps.executeUpdate();
                }
            } catch (SQLException se) {
                // noted as potentionally having other fail points, something to think about later
                // If the UNIQUE constraint (email) has failed, 409 makes sense
                this.conflict("Email already registered");
                return;
            }

            this.sendText(201, "registered coach");
        }
    }

    private static boolean isBlank(String s) {
        return s == null || s.trim().isEmpty();
    }

    private static boolean hasBlankField(Record req) {
        for (var component : req.getClass().getRecordComponents()) {
            try {
                String value = (String) component.getAccessor().invoke(req);
                if (isBlank(value)) {
                    return true;
                }
            } catch (IllegalAccessException e) {
                assert(false);
            } catch (InvocationTargetException e) {
                assert(false);
            }
        }
        return false;
    }
}
