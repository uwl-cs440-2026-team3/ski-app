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

    public record TeamCreateRequest(String name) {};
    public record CourseCreateRequest(String name) {};
    public record ScheduleRequest(String name,
                                  String team_a,
                                  String team_b,
                                  String course,
                                  String start,
                                  String duration) {};
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
    }

    private static class TeamCreateHandler extends
        AuthFlow.PrivilegedHandler<TeamCreateRequest> {
        public TeamCreateHandler(HttpExchange hx) {
            super(hx, TeamCreateRequest.class, "POST");
        }

        @Override
        void handleDetail(TeamCreateRequest req) throws IOException {
            // DB insert
            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                String sql = "INSERT INTO teams (name) VALUES (?)";
                try (PreparedStatement ps = conn.prepareStatement(sql)) {
                    ps.setString(1, req.name);
                    ps.executeUpdate();
                }
            } catch (SQLException se) {
                if (se.getMessage().contains("UNIQUE")) {
                    this.conflict("Team already exists");
                } else {
                    this.sendText(500, se.getMessage());
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
                                conn.prepareStatement("SELECT teamid FROM teams WHERE name = ? AND NOT EXISTS ( SELECT 1 FROM races WHERE (team_id_a = teamid OR team_d_b = teamid) AND endtime > datetime(?, '-30 minutes') AND starttime < datetime(?, ? || ' minutes', '30 minutes'))")) {
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
                String sql = "SELECT email, name, role_mask FROM users";

                // list to hold our gathered members in
                ArrayList<MemberInfo> members = new ArrayList<>();

                // execute our statement
                try (PreparedStatement ps = conn.prepareStatement(sql);
                            ResultSet rs = ps.executeQuery()) {

                    // for each result
                    while (rs.next()) {

                        String role = AuthUtil.getRoleName(rs.getInt("role_mask"));
                        // This endpoint is authenticated - the role mask
                        // must be valid or something is corrupt.
                        assert(!role.equals("noauth"));

                        // add them to the list
                        members.add(new MemberInfo(rs.getString("email"),
                                                   rs.getString("name"),
                                                   role,
                                                   ""));
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
