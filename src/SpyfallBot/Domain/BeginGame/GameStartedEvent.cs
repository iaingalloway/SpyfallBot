namespace SpyfallBot.Domain.BeginGame
{
    using System.Collections.Generic;
    using SpyfallBot.Messaging;

    public sealed class GameStartedEvent : IPublicEvent
    {
        private readonly IEnumerable<string> players;

        public GameStartedEvent(IEnumerable<string> players)
        {
            this.players = players;
        }

        public string Description => $"Let the game begin! The players are:- {string.Join(", ", this.players)}";
    }
}
