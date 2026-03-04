import com.sun.net.httpserver.*;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.net.*;
import java.security.*;
import java.security.cert.CertificateException;
import java.sql.*;
import java.util.Base64;
import java.util.concurrent.CountDownLatch;
import javax.net.ssl.*;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.core.*;

public class Main {
    private final static int port = 1041;
    private final static int connectionBacklog = 5;
    private final static String databaseURL = "jdbc:sqlite:ski.db";
    private final static ObjectMapper JSONMapper = new ObjectMapper();

    public static void main(String[] args) {
        if (args.length < 2) {
            System.err.println("usage: java Main <path_to_certificate> " +
                               "<certificate_passphrase>");
            System.exit(1);
        }

        File serverCert = new File(args[0]);
        char[] passphrase = args[1].toCharArray();
        KeyManager[] keys = Main.loadCertificateOrBust(serverCert, passphrase);
        Main.initDBOrBust(databaseURL);

        try {
            SSLContext ctx = SSLContext.getInstance("TLSv1.2");
            ctx.init(keys, null, null);
            Main.hostServer(ctx);
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace(System.err);
        } catch (UnknownHostException e) {
            e.printStackTrace(System.err);
        } catch (IOException e) {
            e.printStackTrace(System.err);
        } catch (KeyManagementException e) {
            e.printStackTrace(System.err);
        }
    }

    private static class RegisterRequest {
        public String email;
        public String name;
        public String password;
    }

    private static KeyManager[] loadCertificateOrBust(File cert,
            char[] passphrase) {
        try {
            KeyStore ks = KeyStore.getInstance(cert, passphrase);
            KeyManagerFactory kmf = KeyManagerFactory.getInstance("SunX509");
            kmf.init(ks, passphrase);
            return kmf.getKeyManagers();
        } catch (IOException e) {
            System.err.printf("Failed to load key: %s\n", e.getLocalizedMessage());
        } catch (CertificateException e) {
            System.err.printf("Failed to load key: %s\n", e.getLocalizedMessage());
        } catch (KeyStoreException e) {
            System.err.println("No provider for specified keystore.");
        } catch (NoSuchAlgorithmException e) {
            System.err.println("Missing algorithm for keystore integrity check.");
        } catch (UnrecoverableKeyException e) {
            System.err.println("Could not load key. Key file could be corrupt.");
        }

        System.exit(1);
        return null;
    }

    private static void initDBOrBust(String url) {
        // This routine must be called prior to accepting any client
        // connections, to ensure the database exists, and to avoid
        // synchronization issues, as this routine is not threadsafe.
        String sql = """
                     CREATE TABLE IF NOT EXISTS users (
                         userid INTEGER PRIMARY KEY ASC AUTOINCREMENT,
                         email TEXT NOT NULL UNIQUE,
                         name TEXT NOT NULL,
                         pwhash TEXT NOT NULL,
                         role_mask INTEGER DEFAULT 0);
        """;
        try (Connection conn = DriverManager.getConnection(url)) {
            conn.setAutoCommit(true);
            conn.prepareStatement(sql).execute();
        } catch (SQLException e) {
            e.printStackTrace(System.err);
            System.exit(1);
        }
    }

    private static void hostServer(SSLContext ctx) throws UnknownHostException,
        IOException {
        HttpsServer server = Main.getLocalServer();
        server.setHttpsConfigurator(new HttpsConfigurator(ctx));
        Main.registerRoutes(server);
        server.start();

        // The Java 17 documentation does not seem to guarantee that the
        // server thread will keep the process alive. It seems to do so
        // on at least one system, but to be on the safe side, we latch
        // the main thread until the server shuts down to ensure the
        // process does not end prematurely.
        CountDownLatch shutdownLatch = new CountDownLatch(1);
        Runtime.getRuntime().addShutdownHook(new Thread(() -> {
            System.out.println("Shutting down...");
            server.stop(1);
            shutdownLatch.countDown();
        }));

        Main.waitUntilShutdown(shutdownLatch);
    }

    private static HttpsServer getLocalServer() throws UnknownHostException,
        IOException {
        InetAddress hostAddr = InetAddress.getByAddress(new byte[] {0, 0, 0, 0});
        return HttpsServer.create(new InetSocketAddress(hostAddr, Main.port),
                                  Main.connectionBacklog);
    }

    private static void registerRoutes(HttpsServer server) {
        server.createContext("/register",
                             (HttpExchange hx) -> new RegistrationHandler(hx).handle());
        server.createContext("/login",
                             (HttpExchange hx) -> new LoginHandler(hx).handle());
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
                } else if (this.isRecognizedHttpMethod(method)) {
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
                hash = Main.hashPassword(req.password);
            } catch (NoSuchAlgorithmException e) {
                this.sendText(500, "Server misconfigured");
                return;
            }

            try (Connection conn = DriverManager.getConnection(databaseURL)) {
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

    private static class LoginHandler extends RequestLifecycle {
        public LoginHandler(HttpExchange hx) {
            super(hx);
        }

        @Override
        void handleDetail() throws IOException {
            LoginRequest req;
            try {
                req = JSONMapper.readValue(this.hx.getRequestBody(), LoginRequest.class);
            } catch (JacksonException je) {
                this.badRequest("Invalid JSON");
                return;
            }

            try {
                if (this.authenticates(req)) {
                    this.sendBearerToken();
                } else {
                    this.sendFailedLogin();
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

        private boolean authenticates(
            LoginRequest login) throws SQLException, IOException,
            NoSuchAlgorithmException {
            try (Connection conn = DriverManager.getConnection(Main.databaseURL)) {
                PreparedStatement query =
                    conn.prepareStatement("SELECT (pwhash) FROM users WHERE email = ?");
                query.setString(1, login.email);
                ResultSet result = query.executeQuery();

                if (!result.first()) {
                    return false;
                }

                // There can only be zero or one result rows, since emails are
                // constrained to be unique.
                return result.getString("pwhash").equals(Main.hashPassword(login.password));
            }
        }

        private void sendBearerToken() throws IOException, JsonProcessingException {
            String response = Main.JSONMapper.writeValueAsString(this.genToken());
            this.sendText(200, "logged in");
            this.hx.getResponseBody().write(response.getBytes());
        }

        private Token genToken() {
            byte[] token = new byte[256];
            new SecureRandom().nextBytes(token);
            return new Token(Base64.getUrlEncoder().withoutPadding().encodeToString(
                                 token));
        }

        private void sendFailedLogin() throws IOException {
            this.sendText(403, "Unauthorized Credentials");
        }

        private record LoginRequest(String email, String name, String password) {};
        private record Token(String token) {};
    }

    private static String hashPassword(String password) throws
        NoSuchAlgorithmException {
        MessageDigest digest = MessageDigest.getInstance("SHA-256");
        byte[] hash = digest.digest(password.getBytes(StandardCharsets.UTF_8));
        return Base64.getEncoder().encodeToString(hash);
    }

    private static void waitUntilShutdown(CountDownLatch shutdownLatch) {
        // The loop is necessary to make sure we do not terminate due to a
        // supurious wake-up from a thread interrupt.
        while (true) {
            try {
                shutdownLatch.await();
                break;
            } catch (InterruptedException e) {
            }
        }
    }
}
