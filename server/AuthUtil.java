import java.nio.charset.StandardCharsets;
import java.security.*;
import java.util.Base64;
import java.util.regex.*;

public class AuthUtil {
    static Pattern emailRegEx = Pattern.compile(
                                    "^([a-zA-Z0-9!#$%&'*+-/=?^_`{|}~]+(?:.[a-zA-Z0-9!#$%&'*+-/=?^_`{|}~]+)*)" +
                                    "@([a-zA-Z0-9!#$%&'*+-/=?^_`{|}~]+(?:.[a-zA-Z0-9!#$%&'*+-/=?^_`{|}~]+)*)$");

    public static String hashPassword(String password) throws
        NoSuchAlgorithmException {
        MessageDigest digest = MessageDigest.getInstance("SHA-256");
        byte[] hash = digest.digest(password.getBytes(StandardCharsets.UTF_8));
        return Base64.getEncoder().encodeToString(hash);
    }

    public static String getRoleName(int roleMask) {
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

    static boolean isEmailValid(String email) {
        Matcher m = emailRegEx.matcher(email);
        if (!m.matches()) {
            return false;
        }

        if (m.group(0).length() > 64) {
            return false;
        }

        if (m.group(1).length() > 255) {
            return false;
        }

        return true;

    }
}
