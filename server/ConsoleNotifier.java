import java.util.List;

/**
 * A development-mode Notifier that prints messages to standard output
 * instead of sending real emails. Useful when SMTP is not configured
 * or when running tests where you don't want actual email traffic.
 *
 * This is also the default fallback in Main if EmailNotifier setup fails,
 * so the server can still run end-to-end during development.
 */
public class ConsoleNotifier implements Notifier {

    @Override
    public void notifyRaceCanceled(String raceName,
                                   String startTime,
                                   List<Recipient> recipients) {
        System.out.println("==== NOTIFICATION: race canceled ====");
        System.out.printf("  race:  %s%n", raceName);
        System.out.printf("  was:   %s%n", startTime);
        for (Recipient r : recipients) {
            System.out.printf("  -> %s <%s>%n", r.name(), r.email());
        }
        System.out.println("=====================================");
    }

    @Override
    public void notifyRaceRescheduled(String raceName,
                                      String oldStart,
                                      String newStart,
                                      List<Recipient> recipients) {
        System.out.println("==== NOTIFICATION: race rescheduled ====");
        System.out.printf("  race: %s%n", raceName);
        System.out.printf("  old:  %s%n", oldStart);
        System.out.printf("  new:  %s%n", newStart);
        for (Recipient r : recipients) {
            System.out.printf("  -> %s <%s>%n", r.name(), r.email());
        }
        System.out.println("=====================================");
    }

    @Override
    public void notifyRaceReminder(String raceName,
                                   String startTime,
                                   List<Recipient> recipients) {
        System.out.println("==== NOTIFICATION: race reminder ====");
        System.out.printf("  race:  %s%n", raceName);
        System.out.printf("  start: %s%n", startTime);
        for (Recipient r : recipients) {
            System.out.printf("  -> %s <%s>%n", r.name(), r.email());
        }
        System.out.println("=====================================");
    }
}
