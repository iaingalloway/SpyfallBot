namespace SpyfallBot.Messaging.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public interface IObjectValidator
    {
        ObjectValidatorResult TryValidate(object target);
    }

    public sealed class DataAnnotationsObjectValidator : IObjectValidator
    {
        public ObjectValidatorResult TryValidate(object target)
        {
            var context = new ValidationContext(target);

            var results = new List<ValidationResult>();
            var validationSucceeded = Validator.TryValidateObject(target, context, results, true);

            return validationSucceeded ? new ObjectValidatorResult() : new ObjectValidatorResult(results);
        }
    }
}
