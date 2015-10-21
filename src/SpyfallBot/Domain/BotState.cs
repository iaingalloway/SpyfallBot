namespace SpyfallBot.Domain
{
    using System;
    using System.Collections.Generic;
    using SpyfallBot.Domain.BeginGame;
    using SpyfallBot.Domain.EndGame;
    using SpyfallBot.Messaging;

    public sealed class BotState : IAggregate
    {
        private readonly IDictionary<Type, Action<IEvent>> eventMap;

        public BotState()
        {
            this.eventMap = new Dictionary<Type, Action<IEvent>>()
            {
                { typeof(GameStartedEvent), x => this.ApplyGameStartedEvent() },
                { typeof(GameEndedEvent), x => this.ApplyGameEndedEvent() }
            };
        }

        public Game CurrentGame
        {
            get;
            private set;
        }

        public int Version
        {
            get;
            private set;
        }

        public void Apply(IEvent item)
        {
            this.Version++;

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var type = item.GetType();
            if (this.eventMap.ContainsKey(type))
            {
                this.eventMap[type](item);
            }
        }

        private void ApplyGameEndedEvent()
        {
            this.CurrentGame = null;
        }

        private void ApplyGameStartedEvent()
        {
            this.CurrentGame = new Game();
        }
    }
}
