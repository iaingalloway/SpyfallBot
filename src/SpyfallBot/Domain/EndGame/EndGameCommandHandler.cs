namespace SpyfallBot.Domain.EndGame
{
    using System.Collections.Generic;
    using SpyfallBot.Messaging;
    using SpyfallBot.Persistence;

    public sealed class EndGameCommandHandler : ICommandHandler<EndGameCommand>
    {
        private readonly IStore store;

        public EndGameCommandHandler(IStore store)
        {
            this.store = store;
        }

        public IEnumerable<IEvent> Handle(EndGameCommand payload)
        {
            var state = this.store.Load();

            if (state.CurrentGame == null)
            {
                throw new NoGameInProgressException();
            }

            yield return new GameEndedEvent();
        }
    }
}
