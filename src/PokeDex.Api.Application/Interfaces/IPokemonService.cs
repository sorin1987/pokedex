using PokeDex.Api.Domain.Models.Pokemon;
using System.Threading;
using System.Threading.Tasks;

namespace PokeDex.Api.Application.Interfaces
{
    public interface IPokemonService
    {
        Task<Pokemon> GetPokemonByNameAsync(string pokemonName, CancellationToken cancellationToken);
    }
}