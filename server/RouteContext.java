import com.sun.net.httpserver.*;
import java.io.*;
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


    private static abstract class RequestLifecycle {
        protected HttpExchange hx;

        public RequestLifecycle(HttpExchange hx) {
            this.hx = hx;
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

                this.handleDetail();
            } finally {
                this.hx.close();
            }
        }

        abstract void handleDetail() throws IOException;

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

        protected String requireAdmin() throws IOException {
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

    private static class RegistrationHandler extends RequestLifecycle {
        public RegistrationHandler(HttpExchange hx) {
            super(hx);
        }

        @Override
        void handleDetail() throws IOException {

            RegisterRequest req;
            try {
                req = JSONMapper.readValue(this.hx.getRequestBody(), RegisterRequest.class);
            } catch (JacksonException je) {
                this.badRequest("Invalid JSON");
                return;
            }

            if (req == null || isBlank(req.email) || isBlank(req.name)
                    || isBlank(req.password)) {
                this.badRequest("Missing required fields");
                return;
            }

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

    private static boolean isBlank(String s) {
        return s == null || s.trim().isEmpty();
    }

    private static class TeamCreateHandler extends RequestLifecycle {
        public TeamCreateHandler(HttpExchange hx) {
            super(hx);
        }

        @Override
        void handleDetail() throws IOException {
            String role = this.requireAdmin();
            if (role == null) return;
            // Read JSON
            TeamCreateRequest req;
            try {
                req = JSONMapper.readValue(this.hx.getRequestBody(), TeamCreateRequest.class);
            } catch (JacksonException je) {
                this.badRequest("Invalid JSON");
                return;
            }

            if (req == null || isBlank(req.name)) {
                this.badRequest("Missing team name");
                return;
            }

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

        private record TeamCreateRequest(String name) {};
    }

    private static class CourseCreateHandler extends RequestLifecycle {
        public CourseCreateHandler(HttpExchange hx) {
            super(hx);
        }

        @Override
        void handleDetail() throws IOException {

            String role = this.requireAdmin();
            if (role == null) return;

            CourseCreateRequest req;

            try {
                req = JSONMapper.readValue(this.hx.getRequestBody(),
                                           CourseCreateRequest.class);
            } catch (JacksonException je) {
                this.badRequest("Invalid JSON");
                return;
            }

            if (req == null || isBlank(req.name)) {
                this.badRequest("Missing course name");
                return;
            }

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

        private record CourseCreateRequest(String name) {};
    }

    private static class LoginHandler extends RequestLifecycle {
        public LoginHandler(HttpExchange hx) {
            super(hx);
        }

        @Override
        void handleDetail() throws IOException {
            LoginRequest req;
            try {
                req = RouteContext.JSONMapper.readValue(this.hx.getRequestBody(),
                                                        LoginRequest.class);
            } catch (JacksonException je) {
                this.badRequest("Invalid JSON");
                return;
            }

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
            } catch (JsonProcessingException e) {
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
            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                PreparedStatement query =
                    conn.prepareStatement(
                        "SELECT pwhash, role_mask FROM users WHERE email = ?");
                query.setString(1, login.email);
                ResultSet result = query.executeQuery();

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

        private record LoginRequest(String email, String password) {};
        private record LoginResponse(String token, String role) {};
    }

    private static class CoachRegistrationHandler extends RequestLifecycle {
        public CoachRegistrationHandler(HttpExchange hx) {
            super(hx);
        }

        @Override
        void handleDetail() throws IOException {

            // Get the token first
            String role = this.requireAdmin();
            if (role == null) return;

            RegisterRequest req;
            try {
                req = JSONMapper.readValue(this.hx.getRequestBody(), RegisterRequest.class);
            } catch (JacksonException je) {
                this.badRequest("Invalid JSON");
                return;
            }

            if (req == null || isBlank(req.email) || isBlank(req.name)
                    || isBlank(req.password)) {
                this.badRequest("Missing required fields");
                return;
            }

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
}
