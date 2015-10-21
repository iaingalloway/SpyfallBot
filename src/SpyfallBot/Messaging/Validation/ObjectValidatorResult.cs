namespace SpyfallBot.Messaging.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public sealed class ObjectValidatorResult
    {
        public ObjectValidatorResult()
        {
            this.ValidationSucceeded = true;
        }

        public ObjectValidatorResult(IEnumerable<ValidationResult> results)
        {
            this.ValidationSucceeded = false;
            this.Results = results;
        }

        public IEnumerable<ValidationResult> Results
        {
            get;
        }

        public bool ValidationSucceeded
        {
            get;
        }
    }
}
