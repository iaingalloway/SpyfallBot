namespace SpyfallBot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class Memoizer<TIn, TOut>
    {
        private readonly IDictionary<TIn, TOut> cache = new Dictionary<TIn, TOut>();

        private readonly Func<TIn, TOut> factory;

        public Memoizer(Func<TIn, TOut> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.factory = factory;
        }

        public TOut Get(TIn value)
        {
            if (!this.cache.ContainsKey(value))
            {
                this.cache[value] = this.factory(value);
            }

            return this.cache[value];
        }

        public void WarmUp(IEnumerable<TIn> values)
        {
            foreach (var value in values.Where(x => !this.cache.ContainsKey(x)))
            {
                this.cache[value] = this.factory(value);
            }
        }
    }
}
