namespace PokeDex.Api.Application.TranslationProviders
{
    public class ShakespeareTranslationProvider : ITranslationProvider
    {
        public string GetTranslationName()
        {
            return "shakespeare";
        }

        public string Name => nameof(ShakespeareTranslationProvider);
    }
}