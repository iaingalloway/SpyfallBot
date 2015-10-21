namespace SpyfallBot.Persistence
{
    public sealed class ConcurrentCommandException : DomainException
    {
        public ConcurrentCommandException()
            : base("Multiple commands received simultaneously.")
        {
        }
    }
}
