namespace SpyfallBot.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SpyfallBot.Domain;
    using SpyfallBot.Messaging;

    public interface IStore
    {
        BotState Load();

        void Save(int version, IEnumerable<IEvent> newEvents);
    }

    public sealed class InMemoryStore : IStore
    {
        private readonly BotState state;

        public InMemoryStore(BotState state)
        {
            this.state = state;
        }

        public BotState Load() => this.state;

        public void Save(int version, IEnumerable<IEvent> newEvents)
        {
            if (newEvents == null)
            {
                throw new ArgumentNullException(nameof(newEvents));
            }

            if (!newEvents.Any())
            {
                return;
            }

            lock (this.state)
            {
                if (version != this.state.Version)
                {
                    throw new ConcurrentCommandException();
                }

                foreach (var item in newEvents)
                {
                    this.state.Apply(item);
                }
            }
        }
    }
}
