namespace PokeDex.Api.TranslationProviders
{
    public class YodaTranslationProvider : ITranslationProvider
    {
        public string GetTranslationName()
        {
            return "yoda";
        }

        public string Name => nameof(YodaTranslationProvider);
    }
}