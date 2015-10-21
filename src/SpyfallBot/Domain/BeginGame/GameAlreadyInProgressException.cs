namespace SpyfallBot.Domain.BeginGame
{
    public sealed class GameAlreadyInProgressException : DomainException
    {
        public GameAlreadyInProgressException()
            : base("A game is already in progress.")
        {
        }
    }
}
