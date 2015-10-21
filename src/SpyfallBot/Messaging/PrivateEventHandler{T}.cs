namespace SpyfallBot.Messaging
{
    public class PrivateEventHandler<T> : IEventHandler<T>
        where T : IPrivateEvent
    {
        private readonly INotificationService service;

        public PrivateEventHandler(INotificationService service)
        {
            this.service = service;
        }

        public void Handle(T payload)
        {
            this.service.WriteLine(payload.Description, payload.Target);
        }
    }
}
