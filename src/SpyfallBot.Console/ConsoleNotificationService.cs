namespace SpyfallBot.Console
{
    using System;

    public class ConsoleNotificationService : INotificationService
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteLine(string message, string target)
        {
            Console.WriteLine($"{target}: {message}");
        }
    }
}
