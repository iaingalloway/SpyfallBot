namespace SpyfallBot.Messaging
{
    public interface IEvent
    {
    }

    public interface IPublicEvent : IEvent
    {
        string Description
        {
            get;
        }
    }

    public interface IPrivateEvent : IEvent
    {
        string Description
        {
            get;
        }

        string Target
        {
            get;
        }
    }

    public sealed class CommandFailedEvent : IPublicEvent
    {
        public CommandFailedEvent(string description)
        {
            this.Description = description;
        }

        public string Description
        {
            get;
        }
    }
}
