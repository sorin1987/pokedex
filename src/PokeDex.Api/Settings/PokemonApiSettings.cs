namespace PokeDex.Api.Settings
{
    public class PokemonApiSettings
    {
        public string BaseAddress { get; set; }
        public RetrySettings RetrySettings { get; set; }
    }
}