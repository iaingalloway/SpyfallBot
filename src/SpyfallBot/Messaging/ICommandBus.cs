namespace SpyfallBot.Messaging
{
    using System;
    using System.Linq;
    using SpyfallBot.Persistence;

    public interface ICommandBus
    {
        void Send<TCommand>(TCommand payload) where TCommand : class, ICommand;
    }

    public sealed class SynchronousCommandBus : ICommandBus
    {
        private readonly ICommandDispatcher commandDispatcher;

        private readonly IEventBus eventBus;

        private readonly IStore store;

        public SynchronousCommandBus(ICommandDispatcher commandDispatcher, IStore store, IEventBus eventBus)
        {
            this.commandDispatcher = commandDispatcher;
            this.store = store;
            this.eventBus = eventBus;
        }

        public void Send<TCommand>(TCommand payload) where TCommand : class, ICommand
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            var handlers = this.commandDispatcher.GetHandlers<TCommand>();

            var version = this.store.Load().Version;

            try
            {
                var newEvents = handlers.SelectMany(x => x.Handle(payload)).ToList();

                this.store.Save(version, newEvents);

                foreach (var item in newEvents)
                {
                    this.eventBus.Publish(item);
                }
            }
            catch (DomainException e)
            {
                this.eventBus.Publish(new CommandFailedEvent(e.Message));
            }
        }
    }
}
