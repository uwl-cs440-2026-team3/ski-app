import com.sun.net.httpserver.HttpExchange;
import java.io.IOException;
import java.sql.*;
import java.time.LocalDateTime;
import java.time.format.DateTimeParseException;
import java.util.regex.Pattern;

public class ManageRaceFlow {
    private record ScheduleRequest(String name,
                                   String team_a,
                                   String team_b,
                                   String course,
                                   String start,
                                   String duration) {};
    private record PostScoreRequest(String race_name,
                                    String email,
                                    String time) {};

    public static class ScheduleRaceHandler extends
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

    static class PostScoreHandler extends
        AuthFlow.PrivilegedHandler<PostScoreRequest> {

        private static Pattern timeRegEx =
            Pattern.compile("^\\d\\d:\\d\\d\\.\\d$");

        public PostScoreHandler(HttpExchange hx) {
            super(hx, PostScoreRequest.class, "POST");
        }

        @Override
        protected String checkFields(PostScoreRequest req) {
            if (req.race_name.length() > 64) {
                return "Race name too long";
            }

            if (!AuthUtil.isEmailValid(req.email)) {
                return "Invalid email";
            }

            if (!timeRegEx.matcher(req.time).matches()) {
                return "Invalid time";
            }

            return null;
        }

        @Override
        void handleDetail(PostScoreRequest req) {
        }
    }
}
