import com.sun.net.httpserver.*;
import java.io.*;
import java.net.*;
import java.security.*;
import java.security.cert.CertificateException;
import java.sql.*;
import java.util.Base64;
import java.util.concurrent.CountDownLatch;
import javax.net.ssl.*;

public class Main {
    public static void main(String[] args) {
        if (args.length < 2) {
            System.err.println("usage: java Main <path_to_certificate> " +
                               "<certificate_passphrase>");
            System.exit(1);
        }

        File serverCert = new File(args[0]);
        char[] passphrase = args[1].toCharArray();
        KeyManager[] keys = Main.loadCertificateOrBust(serverCert, passphrase);
        Main.initDBOrBust(Config.databaseURL);

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
        String sqlCreateTable = """
                                CREATE TABLE IF NOT EXISTS users (
                                    userid INTEGER PRIMARY KEY ASC AUTOINCREMENT,
                                    email TEXT NOT NULL UNIQUE,
                                    name TEXT NOT NULL,
                                    pwhash TEXT NOT NULL,
                                    role_mask INTEGER DEFAULT 0);

        CREATE TABLE IF NOT EXISTS teams (
            teamid INTEGER PRIMARY KEY ASC AUTOINCREMENT,
            name TEXT NOT NULL UNIQUE,
            skier1_id INTEGER REFERENCES users(userid),
            skier2_id INTEGER REFERENCES users(userid),
            coach_id INTEGER REFERENCES users(userid));

        CREATE TABLE IF NOT EXISTS courses (
            courseid INTEGER PRIMARY KEY ASC AUTOINCREMENT,
            name TEXT NOT NULL UNIQUE);

        """;

        String sqlRegisterAdmin = """
                                  INSERT INTO users (email, name, pwhash, role_mask)
                                  VALUES (?, 'Admin', ?, 1)
                                  ON CONFLICT DO NOTHING;
        """;

        try (Connection conn = DriverManager.getConnection(url)) {
            conn.setAutoCommit(true);
            try (Statement stmt = conn.createStatement()) {
                stmt.executeUpdate(sqlCreateTable);
                PreparedStatement statement = conn.prepareStatement(sqlRegisterAdmin);
                statement.setString(1, Config.adminEmail);
                statement.setString(2, AuthUtil.hashPassword(Config.adminPassword));
                statement.execute();
            }
        } catch (SQLException e) {
            e.printStackTrace(System.err);
            System.exit(1);
        } catch (NoSuchAlgorithmException e) {
            System.err.println("Fatal: Missing hash algorithm.");
            System.exit(1);
        }
    }

    private static void hostServer(SSLContext ctx) throws UnknownHostException,
        IOException {
        HttpsServer server = Main.getLocalServer();
        server.setHttpsConfigurator(new HttpsConfigurator(ctx));
        RouteContext.registerRoutes(server);
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
        return HttpsServer.create(new InetSocketAddress(hostAddr, Config.port),
                                  Config.connectionBacklog);
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
