namespace SpyfallBot.Domain.GetStatus
{
    using SpyfallBot.Messaging;

    public sealed class StatusUpdateEvent : IPublicEvent
    {
        public StatusUpdateEvent(bool gameInProgress)
        {
            this.GameInProgress = gameInProgress;
        }

        public string Description => $"Status! {this.GameInProgressString}";

        private bool GameInProgress
        {
            get;
        }

        private string GameInProgressString
            => this.GameInProgress ? "There's a game in progress" : "There is no game in progress";
    }
}
