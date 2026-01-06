using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics;

namespace DesignPatterns
{
    #region VIOLATION DIP

    // High-level class depends directly on low-level EmailSender
    public class EmailSenderV
    {
        public void SendEmail(string message)
        {
            Console.WriteLine($"[Email] {message}");
        }
    }

    public class NotificationServiceV
    {
        private EmailSenderV _emailSender = new EmailSenderV(); // tightly coupled

        public void Notify(string message)
        {
            _emailSender.SendEmail(message); // depends directly on low-level class
        }
    }

    public class DemoViolation
    {
        public static void Run()
        {
            var service = new NotificationServiceV();
            service.Notify("System update available!"); // only works with EmailSenderV
        }
    }

    #endregion

    #region OBEYING DIP

    // Abstraction
    public interface IMessageSender
    {
        void Send(string message);
    }

    // Low-level implementations
    public class EmailSender : IMessageSender
    {
        public void Send(string message)
        {
            Console.WriteLine($"[Email] {message}");
        }
    }

    public class SmsSender : IMessageSender
    {
        public void Send(string message)
        {
            Console.WriteLine($"[SMS] {message}");
        }
    }

    // High-level class depends on abstraction, not concrete class
    public class NotificationService
    {
        private readonly IMessageSender _sender;

        public NotificationService(IMessageSender sender)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        public void Notify(string message)
        {
            _sender.Send(message);
        }
    }

    public class DemoObeying
    {
        public static void Run()
        {
            IMessageSender emailSender = new EmailSender();
            IMessageSender smsSender = new SmsSender();

            var emailService = new NotificationService(emailSender);
            var smsService = new NotificationService(smsSender);

            emailService.Notify("System update available via Email!");
            smsService.Notify("System update available via SMS!");
        }
    }

    #endregion

    #region DEMO

    public class Demo
    {
        public static void Main()
        {
            Console.WriteLine("=== DIP Violation Demo ===");
            DemoViolation.Run();

            Console.WriteLine("\n=== DIP Obeying Demo ===");
            DemoObeying.Run();

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }

    }

    #endregion
}
