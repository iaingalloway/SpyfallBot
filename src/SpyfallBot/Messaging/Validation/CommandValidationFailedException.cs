namespace SpyfallBot.Messaging.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public sealed class CommandValidationFailedException : DomainException
    {
        public CommandValidationFailedException(IEnumerable<ValidationResult> results)
            : base("That command is invalid.")
        {
            this.Results = results;
        }

        public IEnumerable<ValidationResult> Results
        {
            get;
        }
    }
}
