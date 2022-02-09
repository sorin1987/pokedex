namespace PokeDex.Api.Application.TranslationProviders
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