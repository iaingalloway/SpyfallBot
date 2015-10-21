namespace SpyfallBot.Messaging
{
    public class PublicEventHandler<T> : IEventHandler<T>
        where T : IPublicEvent
    {
        private readonly INotificationService service;

        public PublicEventHandler(INotificationService service)
        {
            this.service = service;
        }

        public void Handle(T payload)
        {
            this.service.WriteLine(payload.Description);
        }
    }
}
