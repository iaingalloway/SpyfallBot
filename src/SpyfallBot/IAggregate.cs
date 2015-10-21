namespace SpyfallBot
{
    using SpyfallBot.Messaging;

    public interface IAggregate
    {
        int Version
        {
            get;
        }

        void Apply(IEvent item);
    }
}
