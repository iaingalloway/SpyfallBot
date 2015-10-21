namespace SpyfallBot.Messaging.Validation
{
    using System.Collections.Generic;

    internal sealed class ValidatingCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> handler;

        private readonly IObjectValidator validator;

        public ValidatingCommandHandler(ICommandHandler<TCommand> handler, IObjectValidator validator)
        {
            this.handler = handler;
            this.validator = validator;
        }

        public IEnumerable<IEvent> Handle(TCommand payload)
        {
            var validationResult = this.validator.TryValidate(payload);

            if (validationResult.ValidationSucceeded)
            {
                return this.handler.Handle(payload);
            }

            throw new CommandValidationFailedException(validationResult.Results);
        }
    }
}
