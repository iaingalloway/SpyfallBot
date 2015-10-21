namespace SpyfallBot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods for collections
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the interfaces implemented by a type which match the specified
        /// open generic type definition
        /// </summary>
        /// <param name="implementingType">
        /// The implementing type
        /// </param>
        /// <param name="interfaceType">
        /// The open generic interface
        /// </param>
        /// <returns>
        /// The closed generic interfaces of the specified open generic type
        /// that the type implements
        /// </returns>
        public static IEnumerable<Type> GetGenericInterfaces(this Type implementingType, Type interfaceType)
        {
            if (implementingType == null)
            {
                throw new ArgumentNullException(nameof(implementingType));
            }

            return
                implementingType.GetInterfaces()
                                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
        }

        /// <summary>
        /// Substitutes the elements of an array of types for the type
        /// parameters of the current generic type definition and returns a
        /// value indicating whether or not the substitution was successful
        /// and the resulting constructed type
        /// </summary>
        /// <param name="openGenericType">
        /// The open generic type to substitute the type parameters into
        /// </param>
        /// <param name="parameters">
        /// An array of types to be substituted for the type parameters of the
        /// generic type
        /// </param>
        /// <returns>
        /// A value indicating whether or not the substitution was successful
        /// and the resulting constructed type
        /// </returns>
        public static TryInvokeResult<Type> TryMakeGenericType(this Type openGenericType, Type[] parameters)
        {
            return TryInvoke<ArgumentException>.Func(() => openGenericType.MakeGenericType(parameters));
        }
    }
}
