namespace PokeDex.Api.Application.Settings
{
    public class ApiClientSettings
    {
        public string BaseAddress { get; set; }
        public RetrySettings RetrySettings { get; set; }
    }
}