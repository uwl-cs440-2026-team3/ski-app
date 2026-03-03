import com.sun.net.httpserver.*;
import java.io.*;
import java.net.*;
import java.security.*;
import java.security.cert.CertificateException;
import java.sql.*;
import java.util.concurrent.CountDownLatch;
import javax.net.ssl.*;
import com.fasterxml.jackson.databind.ObjectMapper;

public class Main {
    private final static int port = 1041;
    private final static int connectionBacklog = 5;
    private final static String databaseURL = "jdbc:sqlite:ski.db";
    private static final ObjectMapper JSONMapper = new ObjectMapper();
    
    
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
        System.out.println("ROUTES REGISTERED");
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
    private static void registerRoutes(HttpsServer server) {
        System.out.println("REGISTER CONTEXT LOADED");
        server.createContext("/register", (HttpExchange hx) -> {
            try {
                if (!hx.getRequestMethod().equals("POST")) {
                    hx.sendResponseHeaders(405, 0);
                    hx.close();
                    return;
                }

                // JSON parse
                RegisterRequest req =
                    JSONMapper.readValue(hx.getRequestBody(), RegisterRequest.class);

                if (req.email == null || req.password == null || req.name == null) {
                    hx.sendResponseHeaders(400, 0);
                    hx.close();
                    return;
                }

                // Password hash
                String hash = hashPassword(req.password);

                // DB insert
                try (Connection conn = DriverManager.getConnection(databaseURL)) {
                    String sql = "INSERT INTO users (email, name, pwhash, role_mask) VALUES (?, ?, ?, ?)";
                    PreparedStatement ps = conn.prepareStatement(sql);
                    ps.setString(1, req.email);
                    ps.setString(2, req.name);
                    ps.setString(3, hash);
                    ps.setInt(4, 0); // şimdilik role 0
                    ps.executeUpdate();
                }

                byte[] response = "registered".getBytes();
                hx.sendResponseHeaders(201, response.length);
                hx.getResponseBody().write(response);

            } catch (SQLException e) {
                hx.sendResponseHeaders(409, 0); // duplicate email
            } catch (Exception e) {
                hx.sendResponseHeaders(400, 0);
            }

            hx.close();
        });
    }
    private static String hashPassword(String password) throws Exception {
        MessageDigest digest = MessageDigest.getInstance("SHA-256");
        byte[] hash = digest.digest(password.getBytes());
        StringBuilder sb = new StringBuilder();
        for (byte b : hash) {
            sb.append(String.format("%02x", b));
        }
        return sb.toString();
    }

}
