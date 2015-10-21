namespace SpyfallBot.Domain.EndGame
{
    using SpyfallBot.Messaging;

    public sealed class GameEndedEvent : IPublicEvent
    {
        public string Description => "The game was ended.";
    }
}
