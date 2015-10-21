namespace SpyfallBot.Domain.BeginGame
{
    using System.Collections.Generic;
    using SpyfallBot.Messaging;
    using SpyfallBot.Persistence;

    public sealed class BeginGameCommandHandler : ICommandHandler<BeginGameCommand>
    {
        private readonly IStore store;

        public BeginGameCommandHandler(IStore store)
        {
            this.store = store;
        }

        public IEnumerable<IEvent> Handle(BeginGameCommand payload)
        {
            var state = this.store.Load();

            if (state.CurrentGame != null)
            {
                throw new GameAlreadyInProgressException();
            }

            yield return new GameStartedEvent(new[] { "Iain" });
        }
    }
}
