import java.util.List;

/**
 * A Notifier that delegates to multiple underlying notifiers in order.
 * Used so the server can both log notifications to the console (handy
 * for debugging and demos) AND persist them to the database for the
 * frontend to fetch — without coupling the rest of the code to either.
 *
 * If any single delegate throws, the failure is logged and we continue
 * to the next one. One broken notifier should never silence the others.
 */
public class CompositeNotifier implements Notifier {

    private final Notifier[] delegates;

    public CompositeNotifier(Notifier... delegates) {
        this.delegates = delegates;
    }

    @Override
    public void notifyRaceCanceled(String raceName,
                                   String startTime,
                                   List<Recipient> recipients) {
        for (Notifier d : delegates) {
            try {
                d.notifyRaceCanceled(raceName, startTime, recipients);
            } catch (RuntimeException e) {
                logFailure(d, "notifyRaceCanceled", e);
            }
        }
    }

    @Override
    public void notifyRaceRescheduled(String raceName,
                                      String oldStart,
                                      String newStart,
                                      List<Recipient> recipients) {
        for (Notifier d : delegates) {
            try {
                d.notifyRaceRescheduled(raceName, oldStart, newStart, recipients);
            } catch (RuntimeException e) {
                logFailure(d, "notifyRaceRescheduled", e);
            }
        }
    }

    @Override
    public void notifyRaceReminder(String raceName,
                                   String startTime,
                                   List<Recipient> recipients) {
        for (Notifier d : delegates) {
            try {
                d.notifyRaceReminder(raceName, startTime, recipients);
            } catch (RuntimeException e) {
                logFailure(d, "notifyRaceReminder", e);
            }
        }
    }

    private static void logFailure(Notifier d, String method, Throwable e) {
        System.err.printf("CompositeNotifier: delegate %s failed in %s: %s%n",
                          d.getClass().getSimpleName(), method, e.getMessage());
    }
}
