namespace SpyfallBot.Console
{
    using System;
    using Ninject;
    using SpyfallBot.Domain.BeginGame;
    using SpyfallBot.Domain.EndGame;
    using SpyfallBot.Domain.GetStatus;
    using SpyfallBot.Messaging;

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var kernel = new StandardKernel())
            using (var messagingConfig = new MessagingConfig())
            {
                kernel.Load(messagingConfig);

                kernel.Bind<INotificationService>().To<ConsoleNotificationService>();

                var commandBus = kernel.Get<ICommandBus>();

                commandBus.Send(new GetStatusCommand());
                commandBus.Send(new BeginGameCommand());
                commandBus.Send(new BeginGameCommand());
                commandBus.Send(new GetStatusCommand());
                commandBus.Send(new EndGameCommand());
                commandBus.Send(new GetStatusCommand());
                commandBus.Send(new EndGameCommand());

                Console.WriteLine(args.Length);
            }

            Console.ReadKey();
        }
    }
}
