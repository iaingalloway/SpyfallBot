namespace SpyfallBot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IImplementingTypesService
    {
        IEnumerable<Type> GetImplementingTypes(Type parent);
    }

    public sealed class TypeFilteringService : IImplementingTypesService
    {
        private readonly ITypeRegistry typeRegistry;

        public TypeFilteringService(ITypeRegistry typeRegistry)
        {
            this.typeRegistry = typeRegistry;
        }

        public IEnumerable<Type> GetImplementingTypes(Type parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            return parent.IsGenericType
                ? this.GetNonGenericImplementingTypes(parent).Concat(this.GetGenericImplementingTypes(parent))
                : this.GetNonGenericImplementingTypes(parent);
        }

        private IEnumerable<Type> GetGenericImplementingTypes(Type parent)
        {
            var definition = parent.GetGenericTypeDefinition();
            var parameters = parent.GetGenericArguments();

            var genericImplementingTypes =
                this.typeRegistry.GenericTypes.Where(x => x.GetGenericInterfaces(definition).Any())
                    .Select(x => x.TryMakeGenericType(parameters))
                    .Where(x => x.Success)
                    .Select(x => x.Value);
            return genericImplementingTypes;
        }

        private IEnumerable<Type> GetNonGenericImplementingTypes(Type parent)
            => this.typeRegistry.Types.Where(x => parent.IsAssignableFrom(x)).Where(x => !x.IsInterface);
    }
}
