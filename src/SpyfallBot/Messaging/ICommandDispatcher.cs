namespace SpyfallBot.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SpyfallBot.Messaging.Validation;

    public interface ICommandDispatcher
    {
        IEnumerable<ICommandHandler<T>> GetHandlers<T>();
    }

    public sealed class ReflectingCommandDispatcher : ICommandDispatcher
    {
        private readonly IImplementingTypesService implementingTypesService;

        private readonly Memoizer<Type, bool> memoizer;

        private readonly IServiceLocator serviceLocator;

        public ReflectingCommandDispatcher(
            IServiceLocator serviceLocator,
            IImplementingTypesService implementingTypesService)
        {
            this.serviceLocator = serviceLocator;
            this.implementingTypesService = implementingTypesService;

            this.memoizer = new Memoizer<Type, bool>(x => x.IsDefined(typeof(ValidateAttribute), false));
        }

        public IEnumerable<ICommandHandler<T>> GetHandlers<T>()
        {
            var implementingTypes =
                this.implementingTypesService.GetImplementingTypes(typeof(ICommandHandler<T>))
                    .Where(x => x != typeof(ValidatingCommandHandler<T>));

            var handlers = implementingTypes.Select(x => this.serviceLocator.Locate(x)).Cast<ICommandHandler<T>>();

            if (!this.memoizer.Get(typeof(T)))
            {
                return handlers;
            }

            var validator = this.serviceLocator.Locate<IObjectValidator>();

            return handlers.Select(x => new ValidatingCommandHandler<T>(x, validator));
        }
    }
}
