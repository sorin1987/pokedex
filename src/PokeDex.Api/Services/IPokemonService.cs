using System.Threading;
using System.Threading.Tasks;
using PokeDex.Api.Domain.Pokemon;

namespace PokeDex.Api.Services
{
    public interface IPokemonService
    {
        Task<Pokemon> GetPokemonByNameAsync(string pokemonName, CancellationToken cancellationToken);
    }
}