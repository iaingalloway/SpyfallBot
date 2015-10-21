namespace SpyfallBot.Messaging
{
    using System;

    public sealed class EventDispatchContext
    {
        public EventDispatchContext(Action<IEvent> handler, Type handlerType)
        {
            this.Handler = handler;
            this.HandlerType = handlerType;
        }

        public Action<IEvent> Handler
        {
            get;
        }

        public Type HandlerType
        {
            get;
        }
    }
}
