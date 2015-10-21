namespace SpyfallBot
{
    using System;
    using System.Collections.Generic;

    public interface ITypeRegistry
    {
        IEnumerable<Type> GenericTypes
        {
            get;
        }

        IEnumerable<Type> Types
        {
            get;
        }
    }
}
