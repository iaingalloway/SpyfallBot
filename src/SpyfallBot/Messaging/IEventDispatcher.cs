namespace SpyfallBot.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IEventDispatcher
    {
        IEnumerable<EventDispatchContext> GetHandlers(Type type);
    }

    public sealed class ReflectingEventDispatcher : IEventDispatcher
    {
        private readonly IImplementingTypesService implementingTypesService;

        private readonly IServiceLocator serviceLocator;

        public ReflectingEventDispatcher(
            IServiceLocator serviceLocator,
            IImplementingTypesService implementingTypesService)
        {
            this.serviceLocator = serviceLocator;
            this.implementingTypesService = implementingTypesService;
        }

        public IEnumerable<EventDispatchContext> GetHandlers(Type type)
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(type);

            var implementingTypes = this.implementingTypesService.GetImplementingTypes(handlerType);

            var adapterType = typeof(IEventHandlerAdapter<>).MakeGenericType(type);
            var adapter = (IEventHandlerAdapter)this.serviceLocator.Locate(adapterType);

            var handlers = implementingTypes.Select(x => this.serviceLocator.Locate(x));

            return handlers.Select(h => new EventDispatchContext(e => adapter.Handle(e, h), h.GetType()));
        }
    }
}
