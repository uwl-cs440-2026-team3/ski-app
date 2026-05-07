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

    /**
     * Notifier used by handlers that need to send race-related alerts
     * (cancellations, reschedules, reminders). Set by Main at startup,
     * defaults to ConsoleNotifier so handlers never see null.
     */
    private static Notifier notifier = new ConsoleNotifier();

    public static void setNotifier(Notifier n) {
        if (n != null) {
            notifier = n;
        }
    }

    public record TeamCreateRequest(String name, String skier1_email,
                                    String skier2_email, String coach_email) {};
    public record CourseCreateRequest(String name) {};
    public record ScheduleRequest(String name,
                                  String team_a,
                                  String team_b,
                                  String course,
                                  String start,
                                  String duration) {};
    public record CancelRaceRequest(String name) {};
    public record MarkNotificationReadRequest(String notificationid) {};
    public record RaceInfo(int raceid,
                           String name,
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
        server.createContext("/cancelrace",
                             (HttpExchange hx) ->
                             new CancelRaceHandler(hx).handle());
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
        server.createContext("/mynotifications",
                             (HttpExchange hx) ->
                             new GetMyNotificationsHandler(hx).handle());
        server.createContext("/marknotificationread",
                             (HttpExchange hx) ->
                             new MarkNotificationReadHandler(hx).handle());
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
        protected String checkFields(ScheduleRequest req) {
            if (req.name.length() > 64) {
                return "Name too long";
            }

            if (req.team_a.equals(req.team_b)) {
                return "Cannot race team against itself";
            }
            if (!this.dateRegEx.matcher(req.start).matches()) {
                return "Invalid start datetime format";
            }
            if (!this.minutesRegEx.matcher(req.duration).matches()) {
                return "Invalid duration";
            }

            try {
                LocalDateTime t = LocalDateTime.parse(req.start);
                if (t.compareTo(LocalDateTime.now()) < 0) {
                    return "Start time is in the past";
                }
            } catch (DateTimeParseException e) {
                assert(false);
            }

            return null;
        }

        @Override
        public void handleDetail(ScheduleRequest req) throws IOException {
            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                // Check active-name uniqueness up front. /schedule and
                // /cancelrace both rely on the invariant that at most one
                // active race exists per name, so we enforce it here.
                // Canceled races sharing this name are fine -- they don't
                // count as active.
                try (PreparedStatement ps = conn.prepareStatement(
                        "SELECT 1 FROM races WHERE name = ? AND status = 'ACTIVE'")) {
                    ps.setString(1, req.name);
                    try (ResultSet rs = ps.executeQuery()) {
                        if (rs.next()) {
                            this.conflict("A race with this name is already active");
                            return;
                        }
                    }
                }

                String sql = """
                             INSERT INTO races
                                 (name, team_id_a, team_id_b, course_id, starttime, endtime)
                             VALUES (
                                 ?,
                                 (SELECT teamid
                                  FROM teams
                                  WHERE name = ?
                                               AND NOT EXISTS (
                                                   SELECT 1
                                                   FROM races
                WHERE (team_id_a = teamid OR team_id_b = teamid)
                                                   AND status = 'ACTIVE'
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
                                                   AND status = 'ACTIVE'
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
                                                   AND status = 'ACTIVE'
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
                                conn.prepareStatement("SELECT teamid FROM teams WHERE name = ? AND NOT EXISTS ( SELECT 1 FROM races WHERE (team_id_a = teamid OR team_id_b = teamid) AND status = 'ACTIVE' AND endtime > datetime(?, '-30 minutes') AND starttime < datetime(?, ? || ' minutes', '30 minutes'))")) {
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
                                conn.prepareStatement("SELECT teamid FROM teams WHERE name = ? AND NOT EXISTS ( SELECT 1 FROM races WHERE (team_id_a = teamid OR team_id_b = teamid) AND status = 'ACTIVE' AND endtime > datetime(?, '-30 minutes') AND starttime < datetime(?, ? || ' minutes', '30 minutes'))")) {
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
                                conn.prepareStatement("SELECT courseid FROM courses WHERE name = ? AND NOT EXISTS ( SELECT 1 FROM races WHERE (course_id = courseid) AND status = 'ACTIVE' AND endtime > datetime(?, '-30 minutes') AND starttime < datetime(?, ? || ' minutes', '30 minutes'))")) {
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
                             SELECT r.raceid,
                             r.name,
                             ta.name AS team_a_name,
                             tb.name AS team_b_name,
                             c.name AS course_name,
                             r.starttime AS start,
                             r.endtime AS end
                             FROM races r
                             JOIN teams ta ON r.team_id_a = ta.teamid
                JOIN teams tb ON r.team_id_b = tb.teamid
                JOIN courses c ON r.course_id = c.courseid
                                                WHERE r.status = 'ACTIVE'
                                                ORDER BY datetime(r.starttime)
                                                """;

                ArrayList<RaceInfo> races = new ArrayList<>();

                try (PreparedStatement ps = conn.prepareStatement(sql);
                            ResultSet rs = ps.executeQuery()) {

                    while (rs.next()) {
                        races.add(new RaceInfo(rs.getInt("raceid"),
                                               rs.getString("name"),
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

    /**
     * Handles POST /cancelrace.
     *
     * Marks a race as canceled by setting its status to 'CANCELED'. The
     * row is preserved (not deleted) so we keep an audit trail and so
     * notifications can reference it. Cancellations are admin-only.
     *
     * The race is identified by name. Because /schedule enforces that
     * only one ACTIVE race may exist with a given name at any time,
     * "the race named X" is unambiguous. If the only race with that
     * name is already canceled, the lookup finds nothing and we return
     * 404 (rather than 409) — there simply is no active race by that
     * name to cancel.
     *
     * After a successful cancellation, all participants (both skiers and
     * the coach of each team) are notified via the configured Notifier.
     * Notification failures are logged but do not affect the HTTP response.
     */
    private static class CancelRaceHandler extends
        AuthFlow.PrivilegedHandler<CancelRaceRequest> {

        public CancelRaceHandler(HttpExchange hx) {
            super(hx, CancelRaceRequest.class, "POST");
        }

        @Override
        protected String checkFields(CancelRaceRequest req) {
            if (req.name.length() > 64) {
                return "Name too long";
            }
            return null;
        }

        @Override
        void handleDetail(CancelRaceRequest req) throws IOException {
            try (Connection conn = DriverManager.getConnection(Config.databaseURL)) {
                conn.setAutoCommit(false);
                try {
                    // Look up the active race by name. The combination
                    // (name, status='ACTIVE') is guaranteed unique by
                    // the uniqueness check in /schedule, so this finds
                    // at most one row.
                    int raceId;
                    String startTime;
                    int teamA;
                    int teamB;
                    try (PreparedStatement ps = conn.prepareStatement(
                            "SELECT raceid, starttime, team_id_a, team_id_b "
                            + "FROM races WHERE name = ? AND status = 'ACTIVE'")) {
                        ps.setString(1, req.name);
                        try (ResultSet rs = ps.executeQuery()) {
                            if (!rs.next()) {
                                conn.rollback();
                                this.sendText(404, "Race not found");
                                return;
                            }
                            raceId = rs.getInt("raceid");
                            startTime = rs.getString("starttime");
                            teamA = rs.getInt("team_id_a");
                            teamB = rs.getInt("team_id_b");
                        }
                    }

                    // Gather recipients BEFORE the update so we have
                    // everything we need to notify even if anything is
                    // changed concurrently after we commit.
                    java.util.List<Notifier.Recipient> recipients =
                            collectRecipients(conn, teamA, teamB);

                    // Flip status to CANCELED. We use raceid here (looked
                    // up above) rather than name, to make the update fully
                    // unambiguous even in the presence of any historical
                    // canceled rows that share this name.
                    try (PreparedStatement ps = conn.prepareStatement(
                            "UPDATE races SET status = 'CANCELED' WHERE raceid = ?")) {
                        ps.setInt(1, raceId);
                        ps.executeUpdate();
                    }

                    conn.commit();

                    // Fire the notification AFTER the commit. We don't
                    // want to send "race canceled" alerts if the DB write
                    // failed.
                    try {
                        RouteContext.notifier.notifyRaceCanceled(
                                req.name, startTime, recipients);
                    } catch (RuntimeException notifyEx) {
                        System.err.println("Notification failed for race \""
                                           + req.name + "\": "
                                           + notifyEx.getMessage());
                    }

                    this.sendText(200, "Race canceled");
                } catch (SQLException inner) {
                    conn.rollback();
                    throw inner;
                }
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }

        /**
         * Collects the email addresses and names of all skiers and coaches
         * on the two teams competing in this race. Used to populate the
         * recipient list for cancellation notifications.
         */
        private java.util.List<Notifier.Recipient> collectRecipients(
                Connection conn, int teamA, int teamB) throws SQLException {
            String sql = """
                    SELECT u.name, u.email
                    FROM users u
                    JOIN teams t ON u.userid IN (t.skier1_id, t.skier2_id, t.coach_id)
                    WHERE t.teamid = ? OR t.teamid = ?
                    """;
            java.util.List<Notifier.Recipient> recipients = new ArrayList<>();
            try (PreparedStatement ps = conn.prepareStatement(sql)) {
                ps.setInt(1, teamA);
                ps.setInt(2, teamB);
                try (ResultSet rs = ps.executeQuery()) {
                    while (rs.next()) {
                        recipients.add(new Notifier.Recipient(
                                rs.getString("name"), rs.getString("email")));
                    }
                }
            }
            return recipients;
        }
    }

    /**
     * Handles GET /mynotifications.
     *
     * Returns the calling user's notifications, newest first. Any logged-in
     * user can call this; each user only sees their own notifications.
     *
     * The frontend can render a notification list, badge, or banner from
     * this response and use POST /marknotificationread to mark items read.
     */
    private static class GetMyNotificationsHandler extends
        AuthFlow.UnprivilegedHandler<NoBodyRequest> {

        public GetMyNotificationsHandler(HttpExchange hx) {
            super(hx, NoBodyRequest.class, "GET");
        }

        @Override
        void handleDetail(NoBodyRequest req) throws IOException {
            // Resolve the calling user from their bearer token. Auth was
            // already checked by UnprivilegedHandler, so we know the
            // token is valid; we just need the email it belongs to.
            String auth = this.hx.getRequestHeaders().getFirst("Authorization");
            String token = auth.substring("Bearer ".length()).trim();
            String email = AuthFlow.getEmailFor(token);
            if (email == null) {
                this.sendText(403, "Invalid session");
                return;
            }

            String sql = """
                SELECT n.notificationid, n.type, n.message,
                       n.created_at, n.read_at
                FROM notifications n
                JOIN users u ON u.userid = n.user_id
                WHERE u.email = ?
                ORDER BY datetime(n.created_at) DESC
                """;

            try (Connection conn = DriverManager.getConnection(Config.databaseURL);
                 PreparedStatement ps = conn.prepareStatement(sql)) {
                ps.setString(1, email);
                try (ResultSet rs = ps.executeQuery()) {
                    java.util.List<NotificationInfo> out = new ArrayList<>();
                    while (rs.next()) {
                        out.add(new NotificationInfo(
                            rs.getInt("notificationid"),
                            rs.getString("type"),
                            rs.getString("message"),
                            rs.getString("created_at"),
                            rs.getString("read_at")));
                    }
                    this.sendText(200, RouteContext.JSONMapper.writeValueAsString(out));
                }
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }

        public record NotificationInfo(int notificationid,
                                       String type,
                                       String message,
                                       String created_at,
                                       String read_at) {};
    }

    /**
     * Handles POST /marknotificationread.
     *
     * Sets the read_at timestamp for a single notification. The user can
     * only mark their own notifications read; attempts to mark someone
     * else's notification return 404 (we don't distinguish "doesn't exist"
     * from "isn't yours" to avoid leaking notification IDs across users).
     */
    private static class MarkNotificationReadHandler extends
        AuthFlow.UnprivilegedHandler<MarkNotificationReadRequest> {

        public MarkNotificationReadHandler(HttpExchange hx) {
            super(hx, MarkNotificationReadRequest.class, "POST");
        }

        @Override
        protected String checkFields(MarkNotificationReadRequest req) {
            try {
                int id = Integer.parseInt(req.notificationid);
                if (id <= 0) {
                    return "notificationid must be positive";
                }
            } catch (NumberFormatException e) {
                return "notificationid must be an integer";
            }
            return null;
        }

        @Override
        void handleDetail(MarkNotificationReadRequest req) throws IOException {
            int notifId = Integer.parseInt(req.notificationid);

            String auth = this.hx.getRequestHeaders().getFirst("Authorization");
            String token = auth.substring("Bearer ".length()).trim();
            String email = AuthFlow.getEmailFor(token);
            if (email == null) {
                this.sendText(403, "Invalid session");
                return;
            }

            // Update the row, but only if it belongs to the calling user.
            // The JOIN-in-WHERE pattern via subquery means rows belonging
            // to other users simply don't match and aren't updated.
            String sql = """
                UPDATE notifications
                SET read_at = ?
                WHERE notificationid = ?
                  AND user_id = (SELECT userid FROM users WHERE email = ?)
                """;

            try (Connection conn = DriverManager.getConnection(Config.databaseURL);
                 PreparedStatement ps = conn.prepareStatement(sql)) {
                ps.setString(1, java.time.Instant.now().toString());
                ps.setInt(2, notifId);
                ps.setString(3, email);
                int updated = ps.executeUpdate();
                if (updated == 0) {
                    this.sendText(404, "Notification not found");
                    return;
                }
                this.sendText(200, "Marked read");
            } catch (SQLException se) {
                this.sendText(500, se.getMessage());
            }
        }
    }
}