using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics;

namespace DesignPatterns
{
    #region VIOLATING LSP
    // Base abstraction
    public abstract class NotificationV
    {
        public abstract void Send(string message);
    }

    // Works fine
    public class EmailNotificationV : NotificationV
    {
        public override void Send(string message)
        {
            Console.WriteLine($"[Email] {message}");
        }
    }

    // Violates LSP: throws exception if message too long
    public class SmsNotificationV : NotificationV
    {
        public override void Send(string message)
        {
            if (message.Length > 20)
                throw new InvalidOperationException("SMS too long!"); // breaks substitution expectation

            Console.WriteLine($"[Email] {message}");
        }
    }

    public class DemoViolation
    {
        public static void Run()
        {
            NotificationV email = new EmailNotificationV();
            NotificationV sms = new EmailNotificationV();

            email.Send("Hello via Email");
            sms.Send("This message is way too long for SMS and will crash");

            Console.ReadKey();
        }
    }
    #endregion

    #region OBEYING LSP
    // Base abstraction
    public abstract class Notification
    {
        public abstract void Send(string message);
    }

    public class EmailNotification : Notification
    {
        public override void Send(string message)
        {
            Console.WriteLine($"[Email] {message}");
        }
    }

    // Obeys LSP: adapts instead of breaking
    public class SmsNotification : Notification
    {
        public override void Send(string message)
        {
           string safeMessage = message.Length > 20
                ? message.Substring(0, 20) + "..."
                : message;

            Console.WriteLine($"[SMS] {safeMessage}");
        }
    }

    public class DemoObeying
    {
        public static void Run()
        {
            Notification email = new EmailNotification();
            Notification sms = new SmsNotification();

            email.Send("Hello via Email!");
            sms.Send("This message is way too long for SMS but still works safely");
        }
    }
    #endregion

    #region DEMO
    public class Demo
    {
        public static void Main()
        {
            Console.WriteLine("=== LSP Violation Demo ===");
            try
            {
                DemoViolation.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\n=== LSP Obeying Demo ===");
            DemoObeying.Run();

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }
    #endregion
}
