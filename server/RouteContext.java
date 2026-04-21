import com.sun.net.httpserver.*;
import java.io.*;
import java.security.*;
import java.sql.*;
import java.util.ArrayList;

import com.fasterxml.jackson.core.*;
import com.fasterxml.jackson.databind.ObjectMapper;

public class RouteContext {
    private final static ObjectMapper JSONMapper = new ObjectMapper();

    public record TeamCreateRequest(String name, String skier1_email,
                                    String skier2_email, String coach_email) {};
    public record CourseCreateRequest(String name) {};
    public record RaceInfo(String name,
                           String teamA,
                           String teamB,
                           String course,
                           String start,
                           String end) {};
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
        server.createContext("/schedule",
                             (HttpExchange hx) ->
                             new ManageRaceFlow.ScheduleRaceHandler(hx).handle());
        server.createContext("/getmembers",
                             (HttpExchange hx) ->
                             new GetMembersHandler(hx).handle());
        server.createContext("/getteams",
                             (HttpExchange hx) ->
                             new GetTeamsHandler(hx).handle());
        server.createContext("/getcourses",
                             (HttpExchange hx) ->
                             new GetCoursesHandler(hx).handle());
        server.createContext("/getraces",
                             (HttpExchange hx) ->
                             new GetRacesHandler(hx).handle());
        server.createContext("/getmyteam",
                             (HttpExchange hx) ->
                             new ViewScheduleFlow.GetMyTeamHandler(hx).handle());
        server.createContext("/getmyraces",
                             (HttpExchange hx) ->
                             new ViewScheduleFlow.GetMyRacesHandler(hx).handle());
    }

    private static class TeamCreateHandler extends
        AuthFlow.PrivilegedHandler<TeamCreateRequest> {
        public TeamCreateHandler(HttpExchange hx) {
            super(hx, TeamCreateRequest.class, "POST");
        }

        @Override
        protected String checkFields(TeamCreateRequest req) {
            if (req.name.length() > 64) {
                return "Name too long";
            }

            if (!AuthUtil.isEmailValid(req.skier1_email)) {
                return "Skier 1 email invalid";
            }

            if (!AuthUtil.isEmailValid(req.skier2_email)) {
                return "Skier 2 email invalid";
            }

            if (!AuthUtil.isEmailValid(req.coach_email)) {
                return "Coach email invalid";
            }

            return null;
        }

        private int getUserIdByEmail(Connection conn,
                                     String email) throws SQLException {
            // Pull only active users (role_mask > 0)
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
                conn.setAutoCommit(false); // Start transaction
                try {
                    // Bring only active users, (role_mask > 0)
                    int skier1Id = getUserIdByEmail(conn, req.skier1_email());
                    int skier2Id = getUserIdByEmail(conn, req.skier2_email());
                    int coachId = getUserIdByEmail(conn, req.coach_email());

                    if (-1 == skier1Id || -1 == skier2Id || -1 == coachId) {
                        this.sendText(400, "Unknown user");
                        return;
                    }

                    String sql =
                        "INSERT INTO teams (name, skier1_id, skier2_id, coach_id) VALUES (?, ?, ?, ?)";
                    try (PreparedStatement ps = conn.prepareStatement(sql)) {
                        ps.setString(1, req.name());
                        ps.setInt(2, skier1Id);
                        ps.setInt(3, skier2Id);
                        ps.setInt(4, coachId);
                        ps.executeUpdate();
                    }

                    conn.commit(); // confirm if it is succesful
                    this.sendText(201, "Team created");

                } catch (SQLException se) {
                    conn.rollback(); // Rollback if an error was found
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
                }
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }
    }

    private static class CourseCreateHandler extends
        AuthFlow.PrivilegedHandler<CourseCreateRequest> {
        public CourseCreateHandler(HttpExchange hx) {
            super(hx, CourseCreateRequest.class, "POST");
        }

        @Override
        protected String checkFields(CourseCreateRequest req) {
            if (req.name.length() > 64) {
                return "Name too long";
            }

            return null;
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

    private static class GetTeamsHandler extends
        AuthFlow.PrivilegedHandler<NoBodyRequest> {

        public GetTeamsHandler(HttpExchange hx) {
            super(hx, NoBodyRequest.class, "GET");
        }

        @Override
        void handleDetail(NoBodyRequest req) throws IOException {

            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql = "SELECT name FROM teams";

                // list to hold our gathered teams in
                ArrayList<String> teams = new ArrayList<>();

                // execute our statement
                try (PreparedStatement ps = conn.prepareStatement(sql);
                            ResultSet rs = ps.executeQuery()) {

                    // for each result
                    while (rs.next()) {
                        // add them to the list
                        teams.add(rs.getString("name"));
                    }
                }

                // jsonify it and send it
                String response = RouteContext.JSONMapper.writeValueAsString(teams);
                this.sendText(200, response);
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }
    }

    private static class GetCoursesHandler extends
        AuthFlow.PrivilegedHandler<NoBodyRequest>     {
        public GetCoursesHandler(HttpExchange hx) {
            super(hx, NoBodyRequest.class, "GET");
        }

        @Override
        void handleDetail(NoBodyRequest req) throws IOException {

            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql = "SELECT name FROM courses";

                // list to hold our gathered Courses in
                ArrayList<String> courses = new ArrayList<>();

                // execute our statement
                try (PreparedStatement ps = conn.prepareStatement(sql);
                            ResultSet rs = ps.executeQuery()) {

                    // for each result
                    while (rs.next()) {
                        // add them to the list
                        courses.add(rs.getString("name"));
                    }
                }

                // jsonify it and send it
                String response = RouteContext.JSONMapper.writeValueAsString(courses);
                this.sendText(200, response);
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }
    }

    private static class GetRacesHandler extends
        AuthFlow.PrivilegedHandler<NoBodyRequest> {
        public GetRacesHandler(HttpExchange hx) {
            super(hx, NoBodyRequest.class, "GET");
        }

        @Override
        void handleDetail(NoBodyRequest req) throws IOException {

            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql = """
                             SELECT r.name,
                             ta.name AS team_a_name,
                             tb.name AS team_b_name,
                             c.name AS course_name,
                             r.starttime AS start,
                             r.endtime AS end
                             FROM races r
                             JOIN teams ta ON r.team_id_a = ta.teamid
                JOIN teams tb ON r.team_id_b = tb.teamid
                JOIN courses c ON r.course_id = c.courseid
                                                ORDER BY datetime(r.starttime)
                                                """;

                ArrayList<RaceInfo> races = new ArrayList<>();

                try (PreparedStatement ps = conn.prepareStatement(sql);
                            ResultSet rs = ps.executeQuery()) {

                    while (rs.next()) {
                        races.add(new RaceInfo(rs.getString("name"),
                                               rs.getString("team_a_name"),
                                               rs.getString("team_b_name"),
                                               rs.getString("course_name"),
                                               rs.getString("start"),
                                               rs.getString("end")));
                    }
                }

                String response = RouteContext.JSONMapper.writeValueAsString(races);
                this.sendText(200, response);
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }
    }
}
