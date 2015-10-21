namespace SpyfallBot.Domain.EndGame
{
    public sealed class NoGameInProgressException : DomainException
    {
        public NoGameInProgressException()
            : base("There is no game in progress.")
        {
        }
    }
}
