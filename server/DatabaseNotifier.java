import java.sql.*;
import java.time.Instant;
import java.util.List;

/**
 * A Notifier implementation that persists notifications to the
 * `notifications` table in the database. The frontend can then fetch
 * them via the /mynotifications endpoint.
 *
 * Each notification is associated with a single user (recipient). When
 * notifyRaceCanceled is called with multiple recipients, this class
 * inserts one row per recipient inside a single transaction.
 *
 * Failures here are caught and logged. They never propagate, because
 * a notification failure must not break the user-facing operation
 * that triggered it (e.g. the cancel-race response code stays 200
 * even if the notification insert fails).
 */
public class DatabaseNotifier implements Notifier {

    private final String databaseURL;

    public DatabaseNotifier(String databaseURL) {
        this.databaseURL = databaseURL;
    }

    @Override
    public void notifyRaceCanceled(String raceName,
                                   String startTime,
                                   List<Recipient> recipients) {
        String message = String.format(
            "Race \"%s\" scheduled for %s has been canceled.",
            raceName, startTime);
        insertForAll("RACE_CANCELED", message, recipients);
    }

    @Override
    public void notifyRaceRescheduled(String raceName,
                                      String oldStart,
                                      String newStart,
                                      List<Recipient> recipients) {
        String message = String.format(
            "Race \"%s\" has been rescheduled from %s to %s.",
            raceName, oldStart, newStart);
        insertForAll("RACE_RESCHEDULED", message, recipients);
    }

    @Override
    public void notifyRaceReminder(String raceName,
                                   String startTime,
                                   List<Recipient> recipients) {
        String message = String.format(
            "Reminder: race \"%s\" starts at %s.",
            raceName, startTime);
        insertForAll("RACE_REMINDER", message, recipients);
    }

    /**
     * Looks up each recipient by email, then inserts a notification row
     * for them. The lookup-by-email pattern matches how other handlers
     * resolve users; we accept the per-recipient query cost in exchange
     * for not having to thread user IDs through the Notifier interface.
     */
    private void insertForAll(String type, String message,
                              List<Recipient> recipients) {
        String createdAt = Instant.now().toString();
        String findUser = "SELECT userid FROM users WHERE email = ?";
        String insertNotif = """
            INSERT INTO notifications (user_id, type, message, created_at)
            VALUES (?, ?, ?, ?)
            """;

        try (Connection conn = DriverManager.getConnection(databaseURL)) {
            conn.setAutoCommit(false);
            try (PreparedStatement findPs = conn.prepareStatement(findUser);
                 PreparedStatement insertPs = conn.prepareStatement(insertNotif)) {

                for (Recipient r : recipients) {
                    findPs.setString(1, r.email());
                    try (ResultSet rs = findPs.executeQuery()) {
                        if (!rs.next()) {
                            // Recipient is referenced by email but no longer
                            // exists. Skip silently rather than failing the
                            // whole batch.
                            System.err.printf(
                                "DatabaseNotifier: skipping unknown recipient %s%n",
                                r.email());
                            continue;
                        }
                        int userId = rs.getInt("userid");

                        insertPs.setInt(1, userId);
                        insertPs.setString(2, type);
                        insertPs.setString(3, message);
                        insertPs.setString(4, createdAt);
                        insertPs.executeUpdate();
                    }
                }
                conn.commit();
            } catch (SQLException inner) {
                conn.rollback();
                throw inner;
            }
        } catch (SQLException e) {
            System.err.printf(
                "DatabaseNotifier: failed to persist %s notification: %s%n",
                type, e.getMessage());
        }
    }
}
