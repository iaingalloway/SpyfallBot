namespace SpyfallBot.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRulebook<in TContext> : IRule<TContext>
    {
    }

    public interface IRule
    {
    }

    public interface IRule<in TContext> : IRule
    {
        void Verify(TContext context);
    }

    public sealed class ReflectingCompositeRulebook<TContext> : IRulebook<TContext>
    {
        private readonly IEnumerable<IRule<TContext>> rules;

        public ReflectingCompositeRulebook(
            IServiceLocator serviceLocator,
            IImplementingTypesService implementingTypesService)
        {
            if (implementingTypesService == null)
            {
                throw new ArgumentNullException(nameof(implementingTypesService));
            }

            var types =
                implementingTypesService.GetImplementingTypes(typeof(IRule<TContext>))
                                        .Where(x => !typeof(IRulebook<TContext>).IsAssignableFrom(x));
            this.rules = types.Select(x => serviceLocator.Locate(x)).Cast<IRule<TContext>>().ToList();
        }

        public void Verify(TContext context)
        {
            foreach (var rule in this.rules)
            {
                rule.Verify(context);
            }
        }
    }
}
