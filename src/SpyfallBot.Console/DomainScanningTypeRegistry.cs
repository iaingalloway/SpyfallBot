namespace SpyfallBot.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class DomainScanningTypeRegistry : ITypeRegistry
    {
        public DomainScanningTypeRegistry()
        {
            this.Types = GetTypesFromCurrentDomain();
            this.GenericTypes = this.Types.Where(x => x.IsGenericType).ToList();
        }

        public IEnumerable<Type> GenericTypes
        {
            get;
        }

        public IEnumerable<Type> Types
        {
            get;
        }

        private static IEnumerable<Type> GetTypesFromCurrentDomain()
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                         .Where(x => !x.FullName.Contains("Microsoft"))
                         .Where(x => !x.FullName.Contains("System"))
                         .Where(x => !x.FullName.Contains("DynamicProxyGenAssembly"))
                         .SelectMany(x => x.GetTypes())
                         .ToList();
        }
    }
}
