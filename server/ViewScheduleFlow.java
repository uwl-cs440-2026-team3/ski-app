import com.sun.net.httpserver.HttpExchange;
import java.io.IOException;
import java.sql.*;

public class ViewScheduleFlow {

    public record NoBodyRequest() {};

    public static class GetMyTeamHandler extends
        AuthFlow.UnprivilegedHandler<NoBodyRequest> {

        public GetMyTeamHandler(HttpExchange hx) {
            super(hx, NoBodyRequest.class, "GET");
        }

        @Override
        void handleDetail(NoBodyRequest req) throws IOException {
            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {

                // Get token
                String auth = this.hx.getRequestHeaders().getFirst("Authorization");
                if (auth == null || !auth.startsWith("Bearer ")) {
                    this.sendText(403, "Missing token");
                    return;
                }
                String token = auth.substring("Bearer ".length()).trim();

                // Find the token that corresponds to the email
                String email = AuthFlow.sessionEmails.get(token);
                if (email == null) {
                    this.sendText(403, "Invalid session");
                    return;
                }

                // Use the email in SQL Query
                String sql = """
                             SELECT t.name
                             FROM teams t
                JOIN users u ON (u.userid = t.skier1_id OR u.userid = t.skier2_id OR u.userid
                = t.coach_id)
                             WHERE u.email = ?
                                             """;

                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, email);
                    ResultSet rs = ps.executeQuery();
                    if (rs.next()) {
                        String response = String.format("{\"teamName\": \"%s\"}",
                                                        rs.getString("name"));
                        this.sendText(200, response);
                    } else {
                        // We can return an empty JSON or a 404. (An empty JSON is safer.)
                        this.sendText(200, "{\"teamName\": \"\"}");
                    }
                }
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }
    }
}
