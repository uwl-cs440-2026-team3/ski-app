import com.sun.net.httpserver.HttpExchange;
import java.io.IOException;
import java.security.*;
import java.sql.*;
import java.util.Base64;
import java.util.concurrent.ConcurrentHashMap;

import com.fasterxml.jackson.core.*;
import com.fasterxml.jackson.databind.ObjectMapper;

public class AuthFlow {
    private static ConcurrentHashMap<String, String> sessions = new
        ConcurrentHashMap<>();
    private final static ObjectMapper JSONMapper = new ObjectMapper();

    private record RegisterRequest(String email, String name, String password) {};
    private record LoginRequest(String email, String password) {};

    public static class RegistrationHandler extends
        RequestLifecycle<RegisterRequest> {

        public RegistrationHandler(HttpExchange hx) {
            super(hx, RegisterRequest.class, "POST");
        }

        @Override
        boolean isAuthorized() throws IOException {
            return true;
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

    public static class CoachRegistrationHandler extends
        AuthFlow.PrivilegedHandler<RegisterRequest> {
        public CoachRegistrationHandler(HttpExchange hx) {
            super(hx, RegisterRequest.class, "POST");
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

    public static class LoginHandler extends
        RequestLifecycle<LoginRequest> {

        public LoginHandler(HttpExchange hx) {
            super(hx, LoginRequest.class, "POST");
        }

        @Override
        boolean isAuthorized() throws IOException {
            return true;
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
                    return AuthUtil.getRoleName(result.getInt("role_mask"));
                } else {
                    return "noauth";
                }
            }
        }

        private void sendLoginSuccess(String role) throws IOException,
            JsonProcessingException {
            String token = this.genToken();
            AuthFlow.sessions.put(token, role);

            LoginResponse responseData = new LoginResponse(token, role);
            String response = AuthFlow.JSONMapper.writeValueAsString(responseData);
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

    public static abstract class PrivilegedHandler<E extends Record>
        extends RequestLifecycle<E> {

        PrivilegedHandler(HttpExchange hx, Class<E> type, String allowMethod) {
            super(hx, type, allowMethod);
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
            String role = AuthFlow.sessions.get(token);
            if (role == null || !role.equals("admin")) {
                this.sendText(403, "Not authorized");
                return null;
            }

            return role;
        }
    }
}
