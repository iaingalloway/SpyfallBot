namespace SpyfallBot.Messaging
{
    using System.Collections.Generic;

    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in T> : ICommandHandler
    {
        IEnumerable<IEvent> Handle(T payload);
    }
}
