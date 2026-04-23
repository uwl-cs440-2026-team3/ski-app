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
                             SELECT t.name,
                             s1.name AS skier1_name,
                             s2.name AS skier2_name,
                             c.name AS coach_name
                             FROM teams t
                JOIN users u ON (u.userid = t.skier1_id OR u.userid = t.skier2_id OR u.userid
                = t.coach_id)
                             JOIN users s1 ON t.skier1_id = s1.userid
                JOIN users s2 ON t.skier2_id = s2.userid
                JOIN users c ON t.coach_id = c.userid
                WHERE u.email = ?
                                               """;

                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, email);
                    ResultSet rs = ps.executeQuery();
                    if (rs.next()) {
                        List<String> skiers = new ArrayList<String>();
                        skiers.add(rs.getString("skier1_name"));
                        skiers.add(rs.getString("skier2_name"));
                        Response resp = new Response(rs.getString("name"),
                                                     skiers,
                                                     rs.getString("coach_name"));
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

    public static class GetMyRacesHandler extends
        AuthFlow.UnprivilegedHandler<NoBodyRequest> {

        public GetMyRacesHandler(HttpExchange hx) {
            super(hx, NoBodyRequest.class, "GET");
        }

        @Override
        void handleDetail(NoBodyRequest req) throws IOException {
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

            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql = """
                             SELECT r.name, ta.name AS team_a_name, tb.name AS team_b_name, c.name AS course_name,  r.starttime AS start, r.endtime AS end
                             FROM users u
                             JOIN teams t
                             ON t.skier1_id = u.userid
                OR t.skier2_id = u.userid
                OR t.coach_id  = u.userid
                                              JOIN races r
                ON r.team_id_a = t.teamid
                OR r.team_id_b = t.teamid
                                              JOIN courses c
                ON c.courseid = r.course_id
                                              JOIN teams ta
                ON ta.teamid = r.team_id_a
                                              JOIN teams tb
                ON tb.teamid = r.team_id_b
                WHERE u.email = ?
                                              AND datetime(r.starttime) >= datetime("now")
                                              ORDER BY datetime(r.starttime);

                """;

                ArrayList<RaceInfo> races = new ArrayList<>();

                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, email);
                    ResultSet rs = ps.executeQuery();

                    while (rs.next()) {
                        races.add(new RaceInfo(rs.getString("name"),
                                               rs.getString("team_a_name"),
                                               rs.getString("team_b_name"),
                                               rs.getString("course_name"),
                                               rs.getString("start"),
                                               rs.getString("end")));
                    }
                }

                String response = JSONMapper.writeValueAsString(races);
                this.sendText(200, response);
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }

        public record RaceInfo(String name,
                               String teamA,
                               String teamB,
                               String course,
                               String start,
                               String end) {};
    }
}
