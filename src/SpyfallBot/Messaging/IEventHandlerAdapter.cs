namespace SpyfallBot.Messaging
{
    using System;

    public interface IEventHandlerAdapter
    {
        void Handle(IEvent payload, object handler);
    }

    public interface IEventHandlerAdapter<TEvent> : IEventHandlerAdapter
        where TEvent : IEvent
    {
    }

    public sealed class CastingEventHandlerAdapter<TEvent> : IEventHandlerAdapter<TEvent>
        where TEvent : class, IEvent
    {
        public void Handle(IEvent payload, object handler)
        {
            var p = payload as TEvent;
            if (p == null)
            {
                throw new InvalidOperationException("Handler cannot handle events of this type");
            }

            var h = handler as IEventHandler<TEvent>;
            if (h == null)
            {
                throw new InvalidOperationException("Handler is not of the correct type");
            }

            h.Handle(p);
        }
    }
}
