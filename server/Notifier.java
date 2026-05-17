import java.util.List;

/**
 * Contract for sending race-related notifications to participants.
 *
 * Implementations of this interface handle the actual delivery
 * mechanism (email, console log, push notification, etc). The rest
 * of the server depends only on this interface — it doesn't care
 * how delivery happens.
 *
 * The Recipient record bundles a name and email so implementations
 * can personalize messages without re-querying the database.
 *
 * All methods should be safe to call from request-handling threads;
 * implementations are expected to handle their own errors and never
 * throw, so a failed notification never breaks the user-facing
 * response. Notification failures are operational concerns and
 * should be logged, not surfaced as HTTP errors.
 */
public interface Notifier {

    record Recipient(String name, String email) {}

    /**
     * Notify recipients that a race has been canceled.
     *
     * @param raceName
     * @param startTime
     * @param recipients
     */
    void notifyRaceCanceled(String raceName,
                            String startTime,
                            List<Recipient> recipients);

    /**
     * Notify recipients that a race has been rescheduled.
     * Reserved for the upcoming reschedule endpoint.
     */
    void notifyRaceRescheduled(String raceName,
                               String oldStart,
                               String newStart,
                               List<Recipient> recipients);

    /**
     * Notify recipients that a race is coming up soon.
     * Reserved for the future reminder scheduler.
     */
    void notifyRaceReminder(String raceName,
                            String startTime,
                            List<Recipient> recipients);
}
