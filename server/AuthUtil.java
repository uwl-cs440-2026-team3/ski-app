import java.nio.charset.StandardCharsets;
import java.security.*;
import java.util.Base64;

public class AuthUtil {
    public static String hashPassword(String password) throws
        NoSuchAlgorithmException {
        MessageDigest digest = MessageDigest.getInstance("SHA-256");
        byte[] hash = digest.digest(password.getBytes(StandardCharsets.UTF_8));
        return Base64.getEncoder().encodeToString(hash);
    }
}
