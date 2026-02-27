import com.sun.net.httpserver.*;
import java.io.IOException;
import java.net.*;
import java.security.NoSuchAlgorithmException;
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
    }

    private static HttpsServer getLocalServer() throws UnknownHostException,
        IOException {
        InetAddress hostAddr = InetAddress.getByAddress(new byte[] {0, 0, 0, 0});
        return HttpsServer.create(new InetSocketAddress(hostAddr, Main.port),
                                  Main.connectionBacklog);
    }
}
