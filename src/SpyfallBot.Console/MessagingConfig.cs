namespace SpyfallBot.Console
{
    using System.Linq;
    using Ninject;
    using Ninject.Modules;
    using SpyfallBot.Messaging;
    using SpyfallBot.Messaging.Validation;
    using SpyfallBot.Persistence;

    internal sealed class MessagingConfig : NinjectModule
    {
        private IImplementingTypesService implementingTypesService;

        public override void Load()
        {
            this.Bind<IServiceLocator>().To<NinjectServiceLocator>();

            this.Bind<ICommandBus>().To<SynchronousCommandBus>();
            this.Bind<IEventBus>().To<SynchronousEventBus>();
            this.Bind<IObjectValidator>().To<DataAnnotationsObjectValidator>().InSingletonScope();

            this.Bind<ICommandDispatcher>().To<ReflectingCommandDispatcher>().InSingletonScope();
            this.Bind<IEventDispatcher>().To<ReflectingEventDispatcher>().InSingletonScope();

            this.Bind<ITypeRegistry>().To<DomainScanningTypeRegistry>().InSingletonScope();

            var service = new TypeFilteringService(this.Kernel.Get<ITypeRegistry>());
            this.Bind<IImplementingTypesService>().ToConstant(service);

            this.Bind<IStore>().To<InMemoryStore>().InSingletonScope();

            this.implementingTypesService = this.Kernel.Get<IImplementingTypesService>();

            this.BindCommandHandlers();

            this.BindEventHandlers();

            this.BindRules();
        }

        private void BindCommandHandlers()
        {
            this.BindConcreteCommandHandlers();
            this.WarmUpCommandHandlerRegistry();
        }

        private void BindConcreteCommandHandlers()
        {
            var concreteHandlers = this.implementingTypesService.GetImplementingTypes(typeof(ICommandHandler));

            foreach (var handler in concreteHandlers)
            {
                this.Bind(handler).To(handler);
            }
        }

        private void BindEventHandlers()
        {
            var types = this.implementingTypesService.GetImplementingTypes(typeof(IEvent));

            foreach (var eventType in types)
            {
                var adapterInterface = typeof(IEventHandlerAdapter<>).MakeGenericType(eventType);
                var adapterImplementation = typeof(CastingEventHandlerAdapter<>).MakeGenericType(eventType);
                this.Bind(adapterInterface).To(adapterImplementation);

                var handlerInterface = typeof(IEventHandler<>).MakeGenericType(eventType);
                var handlerImplementations = this.implementingTypesService.GetImplementingTypes(handlerInterface);

                foreach (var handlerType in handlerImplementations)
                {
                    this.Bind(handlerType).To(handlerType);
                }
            }
        }

        private void BindRules()
        {
            this.Bind(typeof(IRulebook<>)).To(typeof(ReflectingCompositeRulebook<>)).InSingletonScope();

            var types = this.implementingTypesService.GetImplementingTypes(typeof(IRule));

            foreach (var implementation in types)
            {
                this.Bind(implementation).To(implementation).InSingletonScope();
            }
        }

        private void WarmUpCommandHandlerRegistry()
        {
            var commands =
                this.implementingTypesService.GetImplementingTypes(typeof(ICommand))
                    .Where(x => x.GetGenericInterfaces(typeof(ICommand)).FirstOrDefault() != null);

            var handlers = commands.Select(x => typeof(ICommandHandler<>).MakeGenericType(x));

            foreach (var handler in handlers)
            {
                this.implementingTypesService.GetImplementingTypes(handler);
            }
        }
    }
}
