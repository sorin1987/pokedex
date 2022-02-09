namespace PokeDex.Api.Application.TranslationProviders
{
    public interface ITranslationProvider
    {
        string GetTranslationName();
        public string Name { get; }
    }
}