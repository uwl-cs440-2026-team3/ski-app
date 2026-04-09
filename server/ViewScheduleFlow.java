import com.sun.net.httpserver.HttpExchange;
import java.io.IOException;
import java.sql.*;
import java.util.*;

import com.fasterxml.jackson.databind.ObjectMapper;

public class ViewScheduleFlow {
    private final static ObjectMapper JSONMapper = new ObjectMapper();

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
                String email = AuthFlow.getEmailFor(token);
                if (email == null) {
                    this.sendText(403, "Invalid session");
                    return;
                }

                // Use the email in SQL Query
                String sql = """
                             SELECT t.name, t.skier
                             FROM teams t
                JOIN users u ON (u.userid = t.skier1_id OR u.userid = t.skier2_id OR u.userid
                = t.coach_id)
                             WHERE u.email = ?
                                             """;

                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, email);
                    ResultSet rs = ps.executeQuery();
                    if (rs.next()) {
                        List<String> skiers = new ArrayList<String>();
                        Response resp = new Response(rs.getString("name"),
                                                     skiers,
                                                     "");
                        String text = JSONMapper.writeValueAsString(resp);
                        this.sendText(200, text);
                    } else {
                        // The API reference specifies a 404 Not Found
                        // response when the user has no team.
                        this.sendText(404, "");
                    }
                }
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }

        private record Response(String name, List<String> skiers, String coach) {};

    }
}
