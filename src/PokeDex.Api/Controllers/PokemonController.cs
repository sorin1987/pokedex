using System.Threading;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PokeDex.Api.Contracts;
using PokeDex.Api.Domain;

namespace PokeDex.Api.Controllers
{
    [Route(ApiRoutes.Pokemon.Root)]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        [HttpGet(ApiRoutes.Pokemon.GetByName)]
        public async Task<IActionResult> GetPokemonAsync([FromRoute]string pokemonName, CancellationToken cancellationToken)
        {
            var response = new Pokemon
            {
                Name = "pikachu", Description = "electric pokemon", Habitat = "forest", IsLegendary = true
            };

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Pokemon.GetTranslatedByName)]
        public async Task<IActionResult> GetTranslatedPokemonAsync([FromRoute]string pokemonName, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
