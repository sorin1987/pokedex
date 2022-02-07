using System.Threading;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using PokeDex.Api.Contracts;
using PokeDex.Api.Exceptions;
using PokeDex.Api.Responses;
using PokeDex.Api.Services;

namespace PokeDex.Api.Controllers
{
    [Route(ApiRoutes.Pokemon.Root)]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonService pokemonService, IMapper mapper)
        {
            _pokemonService = pokemonService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Pokemon.GetByName)]
        public async Task<IActionResult> GetPokemonAsync([FromRoute]string pokemonName, CancellationToken cancellationToken)
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
        public async Task<IActionResult> GetTranslatedPokemonAsync([FromRoute]string pokemonName, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
