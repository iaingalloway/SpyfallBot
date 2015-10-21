namespace SpyfallBot.Messaging
{
    using System;

    public interface IServiceLocator
    {
        T Locate<T>();

        object Locate(Type type);
    }
}
