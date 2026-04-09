import com.sun.net.httpserver.*;
import java.io.*;
import java.security.*;
import java.sql.*;
import java.util.ArrayList;

import com.fasterxml.jackson.core.*;
import com.fasterxml.jackson.databind.ObjectMapper;

public class RouteContext {
    private final static ObjectMapper JSONMapper = new ObjectMapper();

    public record TeamCreateRequest(String name, String skier1_email, String skier2_email, String coach_email) {};
    public record CourseCreateRequest(String name) {};
    public record NoBodyRequest() {};

    public static void registerRoutes(HttpsServer server) {
        server.createContext("/register",
                             (HttpExchange hx) ->
                             new AuthFlow.RegistrationHandler(hx).handle());
        server.createContext("/registercoach",
                             (HttpExchange hx) ->
                             new AuthFlow.CoachRegistrationHandler(hx).handle());
        server.createContext("/login",
                             (HttpExchange hx) ->
                             new AuthFlow.LoginHandler(hx).handle());
        server.createContext("/team",
                             (HttpExchange hx) ->
                             new TeamCreateHandler(hx).handle());
        server.createContext("/course",
                             (HttpExchange hx) ->
                             new CourseCreateHandler(hx).handle());
        server.createContext("/getmembers",
                             (HttpExchange hx) ->
                             new GetMembersHandler(hx).handle());
    }

    private static class TeamCreateHandler extends
        AuthFlow.PrivilegedHandler<TeamCreateRequest> {
        public TeamCreateHandler(HttpExchange hx) {
            super(hx, TeamCreateRequest.class, "POST");
        }

        private int getUserIdByEmail(Connection conn, String email) throws SQLException {
            String sql = "SELECT userid FROM users WHERE email = ?";
            try (PreparedStatement ps = conn.prepareStatement(sql)) {
                ps.setString(1, email);
                try (ResultSet rs = ps.executeQuery()) {
                    if (rs.next()) return rs.getInt("userid");
                }
            }
            return -1;
        }

        @Override
        void handleDetail(TeamCreateRequest req) throws IOException {
            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                int skier1Id = getUserIdByEmail(conn, req.skier1_email());
                int skier2Id = getUserIdByEmail(conn, req.skier2_email());
                int coachId = getUserIdByEmail(conn, req.coach_email());

                if (skier1Id == -1 || skier2Id == -1 || coachId == -1) {
                    this.sendText(400, "One or more users (skiers or coach) do not exist.");
                    return;
                }

                String sql = "INSERT INTO teams (name, skier1_id, skier2_id, coach_id) VALUES (?, ?, ?, ?)";
                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, req.name());
                    ps.setInt(2, skier1Id);
                    ps.setInt(3, skier2Id);
                    ps.setInt(4, coachId);
                    ps.executeUpdate();
                }
            } catch (SQLException se) {
                String msg = se.getMessage();
                if (msg.contains("teams.name")) {
                    this.conflict("Team name already exists");
                } else if (msg.contains("skier1_id") || msg.contains("skier2_id")) {
                    this.conflict("One of the skiers is already in a team");
                } else if (msg.contains("coach_id")) {
                    this.conflict("The coach is already assigned to a team");
                } else {
                    this.sendText(500, msg);
                }
            return;
            }

            this.sendText(201, "Team created");
        }
    }

    private static class CourseCreateHandler extends
        AuthFlow.PrivilegedHandler<CourseCreateRequest> {
        public CourseCreateHandler(HttpExchange hx) {
            super(hx, CourseCreateRequest.class, "POST");
        }

        @Override
        void handleDetail(CourseCreateRequest req) throws IOException {
            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql = "INSERT INTO courses (name) VALUES (?)";

                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, req.name);
                    ps.executeUpdate();
                }
            } catch (SQLException se) {
                if (se.getMessage().contains("UNIQUE")) {
                    this.conflict("Course already exists");
                } else {
                    this.sendText(500, se.getMessage());
                }

                return;
            }

            this.sendText(201, "Course created");
        }
    }

    private static class GetMembersHandler extends
        AuthFlow.PrivilegedHandler<NoBodyRequest> {
        public GetMembersHandler(HttpExchange hx) {
            super(hx, NoBodyRequest.class, "GET");
        }

        @Override
        void handleDetail(NoBodyRequest req) throws IOException {

            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql = """
                             SELECT u.email, u.name, u.role_mask, COALESCE(t.name, '') as team_name 
                             FROM users u
                             LEFT JOIN teams t ON u.userid IN (t.skier1_id, t.skier2_id, t.coach_id)
                             """;

                ArrayList<MemberInfo> members = new ArrayList<>();

                try (PreparedStatement ps = conn.prepareStatement(sql);
                            ResultSet rs = ps.executeQuery()) {

                    while (rs.next()) {
                        String role = AuthUtil.getRoleName(rs.getInt("role_mask"));
                        assert(!role.equals("noauth"));

                        members.add(new MemberInfo(rs.getString("email"),
                                                   rs.getString("name"),
                                                   role,
                                                   rs.getString("team_name")));
                    }
                }

                // jsonify it and send it
                String response = RouteContext.JSONMapper.writeValueAsString(members);
                this.sendText(200, response);
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }

        private record MemberInfo(String email, String name, String role,
                                  String team) {}
    }
}
