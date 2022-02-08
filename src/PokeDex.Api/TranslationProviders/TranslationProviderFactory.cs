using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PokeDex.Api.Responses;
using PokeDex.Api.Services;

namespace PokeDex.Api.TranslationProviders
{
    public interface ITranslationProviderFactory
    {
        ITranslationProvider GetTranslationProvider(PokemonResponse pokemon);
        Task<string> ApplyTranslationAsync(ITranslationProvider translationProvider, string text, CancellationToken cancellationToken);
    }

    public class TranslationProviderFactory : ITranslationProviderFactory
    {
        private readonly ITranslationService _translationService;
        private readonly IReadOnlyDictionary<string, ITranslationProvider> _translationProviders;

        public TranslationProviderFactory(ITranslationService translationService)
        {
            _translationService = translationService;
            _translationProviders = new Dictionary<string, ITranslationProvider>
            {
                { nameof(YodaTranslationProvider), new YodaTranslationProvider() },
                { nameof(ShakespeareTranslationProvider), new ShakespeareTranslationProvider() }
            };
        }

        public ITranslationProvider GetTranslationProvider(PokemonResponse pokemon)
        {
            return pokemon.IsLegendary || pokemon.Habitat == "cave"
                ? _translationProviders[nameof(YodaTranslationProvider)]
                : _translationProviders[nameof(ShakespeareTranslationProvider)];
        }

        public async Task<string> ApplyTranslationAsync(ITranslationProvider translationProvider, string text, CancellationToken cancellationToken)
        {
            return await _translationService.TranslateTextAsync(text, translationProvider.GetTranslationName(), cancellationToken);
        }
    }
}