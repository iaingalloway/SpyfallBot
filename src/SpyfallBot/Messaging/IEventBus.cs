namespace SpyfallBot.Messaging
{
    using System;

    public interface IEventBus
    {
        void Publish(IEvent payload);
    }

    public sealed class SynchronousEventBus : IEventBus
    {
        private readonly IEventDispatcher eventDispatcher;

        public SynchronousEventBus(IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public void Publish(IEvent payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            var handlers = this.eventDispatcher.GetHandlers(payload.GetType());

            foreach (var context in handlers)
            {
                context.Handler(payload);
            }
        }
    }
}
