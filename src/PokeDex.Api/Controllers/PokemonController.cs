using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokeDex.Api.Application.Interfaces;
using PokeDex.Api.Application.TranslationProviders;
using PokeDex.Api.Contracts;
using PokeDex.Api.Contracts.Responses;
using PokeDex.Api.Domain.Exceptions;
using PokeDex.Api.Filters;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace PokeDex.Api.Controllers
{
    [Route(ApiRoutes.Pokemon.Root)]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly ITranslationProviderFactory _translationProviderFactory;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonService pokemonService, IMapper mapper, ITranslationProviderFactory translationProviderFactory)
        {
            _pokemonService = pokemonService;
            _mapper = mapper;
            _translationProviderFactory = translationProviderFactory;
        }

        [HttpGet(ApiRoutes.Pokemon.GetByName)]
        [Cached(600)]
        public async Task<IActionResult> GetPokemonAsync([FromRoute, Required] string pokemonName, CancellationToken cancellationToken)
        {
            try
            {
                var pokemon = await _pokemonService.GetPokemonByNameAsync(pokemonName, cancellationToken);
                var pokemonResponse = _mapper.Map<PokemonResponse>(pokemon);
                return Ok(pokemonResponse);
            }
            catch (PokemonApiException)
            {
                return NotFound();
            }
        }

        [HttpGet(ApiRoutes.Pokemon.GetTranslatedByName)]
        [Cached(600)]
        public async Task<IActionResult> GetTranslatedPokemonAsync([FromRoute, Required] string pokemonName, CancellationToken cancellationToken)
        {
            try
            {
                var pokemon = await _pokemonService.GetPokemonByNameAsync(pokemonName, cancellationToken);
                var pokemonResponse = _mapper.Map<PokemonResponse>(pokemon);
                
                var translationProvider = _translationProviderFactory.GetTranslationProvider(pokemonResponse);
                var translatedDescription = await _translationProviderFactory.ApplyTranslationAsync(translationProvider, pokemonResponse.Description, cancellationToken);
                pokemonResponse.Description = translatedDescription;
                
                return Ok(pokemonResponse);
            }
            catch (PokemonApiException)
            {
                return NotFound();
            }
        }
    }
}
