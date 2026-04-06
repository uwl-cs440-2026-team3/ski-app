import com.sun.net.httpserver.*;
import java.io.*;
import java.security.*;
import java.sql.*;
import java.time.LocalDateTime;
import java.time.format.DateTimeParseException;
import java.util.ArrayList;
import java.util.regex.Pattern;

import com.fasterxml.jackson.core.*;
import com.fasterxml.jackson.databind.ObjectMapper;

public class RouteContext {
    private final static ObjectMapper JSONMapper = new ObjectMapper();

    public record TeamCreateRequest(String name, String skier1_email,
                                    String skier2_email, String coach_email) {};
    public record CourseCreateRequest(String name) {};
    public record ScheduleRequest(String name,
                                  String team_a,
                                  String team_b,
                                  String course,
                                  String start,
                                  String duration) {};
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
                             new ScheduleRaceHandler(hx).handle());
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
    }

    private static class TeamCreateHandler extends
        AuthFlow.PrivilegedHandler<TeamCreateRequest> {
        public TeamCreateHandler(HttpExchange hx) {
            super(hx, TeamCreateRequest.class, "POST");
        }

        private int getUserIdByEmail(Connection conn,
                                     String email) throws SQLException {
            // Pull only active users (role_mask > 0)
            String sql = "SELECT userid FROM users WHERE email = ? AND role_mask > 0";
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

    private static class ScheduleRaceHandler extends
        AuthFlow.PrivilegedHandler<ScheduleRequest> {

        Pattern dateRegEx;
        Pattern minutesRegEx;

        public ScheduleRaceHandler(HttpExchange hx) {
            super(hx, ScheduleRequest.class, "POST");
            this.dateRegEx = Pattern.compile("^\\d{4}-\\d\\d-\\d\\dT\\d\\d:\\d\\d$");
            this.minutesRegEx = Pattern.compile("^\\d+$");
        }

        @Override
        public void handleDetail(ScheduleRequest req) throws IOException {
            if (req.team_a.equals(req.team_b)) {
                this.sendText(400, "cannot race team against itself");
                return;
            }
            if (!this.dateRegEx.matcher(req.start).matches()) {
                this.sendText(400, "invalid start datetime format");
                return;
            }
            if (!this.minutesRegEx.matcher(req.duration).matches()) {
                this.sendText(400, "invalid duration");
                return;
            }

            try {
                LocalDateTime t = LocalDateTime.parse(req.start);
                if (t.compareTo(LocalDateTime.now()) < 0) {
                    this.sendText(400, "start time is in the past");
                    return;
                }
            } catch (DateTimeParseException e) {
                assert(false);
            }

            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql = """
                             INSERT INTO races VALUES (
                                 ?,
                                 (SELECT teamid
                                  FROM teams
                                  WHERE name = ?
                                               AND NOT EXISTS (
                                                   SELECT 1
                                                   FROM races
                WHERE (team_id_a = teamid OR team_id_b = teamid)
                                                   AND endtime > datetime(?, "-30 minutes")
                                                   AND starttime < datetime(?, ? || " minutes", "30 minutes")
                                               )
                                 ),
                                 (SELECT teamid
                                  FROM teams
                                  WHERE name = ?
                                               AND NOT EXISTS (
                                                   SELECT 1
                                                   FROM races
                WHERE (team_id_a = teamid OR team_id_b = teamid)
                                                   AND endtime > datetime(?, "-30 minutes")
                                                   AND starttime < datetime(?, ? || " minutes", "30 minutes")
                                               )
                                 ),
                                 (SELECT courseid
                                  FROM courses
                                  WHERE name = ?
                                               AND NOT EXISTS (
                                                   SELECT 1
                                                   FROM races
                                                   WHERE (course_id = courseid)
                                                   AND endtime > datetime(?, "-30 minutes")
                                                   AND starttime < datetime(?, ? || " minutes", "30 minutes")
                                               )
                                 ),
                                 datetime(?),
                                 datetime(?, ? || " minutes"));
                """;

                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, req.name);
                    ps.setString(2, req.team_a);
                    ps.setString(3, req.start);
                    ps.setString(4, req.start);
                    ps.setString(5, req.duration);

                    ps.setString(6, req.team_b);
                    ps.setString(7, req.start);
                    ps.setString(8, req.start);
                    ps.setString(9, req.duration);
                    ps.setString(10, req.course);
                    ps.setString(11, req.start);
                    ps.setString(12, req.start);
                    ps.setString(13, req.duration);
                    ps.setString(14, req.start);
                    ps.setString(15, req.start);
                    ps.setString(16, req.duration);
                    ps.executeUpdate();
                }
            } catch (SQLException se) {
                diagnose(req, se);
            }

            this.sendText(201, "Created");
        }

        private void diagnose(ScheduleRequest req,
                              SQLException originalException) throws IOException {

            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {

                try (PreparedStatement ps =
                                conn.prepareStatement("SELECT 1 FROM teams WHERE name = ?")) {
                    ps.setString(1, req.team_a);
                    ResultSet rs = ps.executeQuery();

                    if (!rs.next()) {
                        this.sendText(400, "team_a not found");
                        return;
                    }
                }

                try (PreparedStatement ps =
                                conn.prepareStatement("SELECT 1 FROM teams WHERE name = ?")) {
                    ps.setString(1, req.team_b);
                    ResultSet rs = ps.executeQuery();

                    if (!rs.next()) {
                        this.sendText(400, "team_b not found");
                        return;
                    }
                }

                try (PreparedStatement ps =
                                conn.prepareStatement("SELECT 1 FROM courses WHERE name = ?")) {
                    ps.setString(1, req.course);
                    ResultSet rs = ps.executeQuery();

                    if (!rs.next()) {
                        this.sendText(400, "course not found");
                        return;
                    }
                }


                try (PreparedStatement ps =
                                conn.prepareStatement("SELECT teamid FROM teams WHERE name = ? AND NOT EXISTS ( SELECT 1 FROM races WHERE (team_id_a = teamid OR team_id_b = teamid) AND endtime > datetime(?, '-30 minutes') AND starttime < datetime(?, ? || ' minutes', '30 minutes'))")) {
                    ;

                    ps.setString(1, req.team_a);
                    ps.setString(2, req.start);
                    ps.setString(3, req.start);
                    ps.setString(4, req.duration);
                    ResultSet rs = ps.executeQuery();

                    if (!rs.next()) {
                        this.sendText(409, "team_a conflicts");
                        return;
                    }
                }

                try (PreparedStatement ps =
                                conn.prepareStatement("SELECT teamid FROM teams WHERE name = ? AND NOT EXISTS ( SELECT 1 FROM races WHERE (team_id_a = teamid OR team_id_b = teamid) AND endtime > datetime(?, '-30 minutes') AND starttime < datetime(?, ? || ' minutes', '30 minutes'))")) {
                    ;

                    ps.setString(1, req.team_b);
                    ps.setString(2, req.start);
                    ps.setString(3, req.start);
                    ps.setString(4, req.duration);
                    ResultSet rs = ps.executeQuery();

                    if (!rs.next()) {
                        this.sendText(409, "team_b conflicts");
                        return;
                    }
                }

                try (PreparedStatement ps =
                                conn.prepareStatement("SELECT courseid FROM courses WHERE name = ? AND NOT EXISTS ( SELECT 1 FROM races WHERE (course_id = courseid) AND endtime > datetime(?, '-30 minutes') AND starttime < datetime(?, ? || ' minutes', '30 minutes'))")) {
                    ;

                    ps.setString(1, req.course);
                    ps.setString(2, req.start);
                    ps.setString(3, req.start);
                    ps.setString(4, req.duration);
                    ResultSet rs = ps.executeQuery();

                    if (!rs.next()) {
                        this.sendText(409, "course conflicts");
                        return;
                    }
                }

                this.sendText(503, originalException.getMessage());
            } catch (SQLException se) {
                this.sendText(500, se.getMessage() +
                              "while processing: " +
                              originalException.getMessage());
            }

            return;
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
                ArrayList<CourseInfo> courses = new ArrayList<>();

                // execute our statement
                try (PreparedStatement ps = conn.prepareStatement(sql);
                            ResultSet rs = ps.executeQuery()) {

                    // for each result
                    while (rs.next()) {
                        // add them to the list
                        courses.add(new CourseInfo(rs.getString("name")));
                    }
                }

                // jsonify it and send it
                String response = RouteContext.JSONMapper.writeValueAsString(courses);
                this.sendText(200, response);
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }

        private record CourseInfo(String name) {}
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
