using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using PokeDex.Api.Domain;
using PokeDex.Api.Exceptions;

namespace PokeDex.Api.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PokemonService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Pokemon> GetPokemonByNameAsync(string pokemonName, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("PokemonApi");
            var response = await httpClient.GetAsync($"{pokemonName}", cancellationToken);
            
            if (!response.IsSuccessStatusCode)
                throw new PokemonApiException();
            
            var stream = await response.Content.ReadAsStreamAsync();
            var pokemon = await JsonSerializer.DeserializeAsync<Pokemon>(stream, cancellationToken: cancellationToken);
            
            return pokemon;
        }
    }
}