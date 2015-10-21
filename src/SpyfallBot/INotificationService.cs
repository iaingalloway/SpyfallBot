namespace SpyfallBot
{
    public interface INotificationService
    {
        void WriteLine(string message);

        void WriteLine(string message, string target);
    }
}
