namespace PokeDex.Api.Settings
{
    public class RetrySettings
    {
        public int MaxRetries { get; set; }
        public int WaitBetweenRetriesFactorMilliseconds { get; set; }
    }
}