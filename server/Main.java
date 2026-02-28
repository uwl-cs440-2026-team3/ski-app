import com.sun.net.httpserver.*;
import java.io.IOException;
import java.net.*;
import java.security.NoSuchAlgorithmException;
import java.util.concurrent.CountDownLatch;
import javax.net.ssl.SSLContext;

public class Main {
    private final static int port = 1041;
    private final static int connectionBacklog = 5;

    public static void main(String[] args) {
        try {
            Main.hostServer(SSLContext.getDefault());
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace(System.err);
        } catch (UnknownHostException e) {
            e.printStackTrace(System.err);
        } catch (IOException e) {
            e.printStackTrace(System.err);
        }
    }

    private static void hostServer(SSLContext ctx) throws UnknownHostException,
        IOException {
        HttpsServer server = Main.getLocalServer();
        server.setHttpsConfigurator(new HttpsConfigurator(ctx));
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
}
