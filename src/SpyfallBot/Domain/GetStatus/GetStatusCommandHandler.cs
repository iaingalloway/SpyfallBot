namespace SpyfallBot.Domain.GetStatus
{
    using System.Collections.Generic;
    using SpyfallBot.Messaging;
    using SpyfallBot.Persistence;

    public class GetStatusCommandHandler : ICommandHandler<GetStatusCommand>
    {
        private readonly IStore store;

        public GetStatusCommandHandler(IStore store)
        {
            this.store = store;
        }

        public IEnumerable<IEvent> Handle(GetStatusCommand payload)
        {
            var state = this.store.Load();

            yield return new StatusUpdateEvent(state.CurrentGame != null);
        }
    }
}
