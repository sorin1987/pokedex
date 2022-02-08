namespace PokeDex.Api.TranslationProviders
{
    public interface ITranslationProvider
    {
        string GetTranslationName();
        public string Name { get; }
    }
}