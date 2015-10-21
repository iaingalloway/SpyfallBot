namespace SpyfallBot.Messaging
{
    public class CommandContext<TCommand>
    {
        public CommandContext(TCommand command)
        {
            this.Command = command;
        }

        public TCommand Command
        {
            get;
        }
    }
}
