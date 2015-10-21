namespace SpyfallBot.Console
{
    using System;
    using Ninject;
    using SpyfallBot.Messaging;

    public class NinjectServiceLocator : IServiceLocator
    {
        private readonly IKernel kernel;

        public NinjectServiceLocator(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public T Locate<T>()
        {
            return this.kernel.Get<T>();
        }

        public object Locate(Type type)
        {
            return this.kernel.Get(type);
        }
    }
}
