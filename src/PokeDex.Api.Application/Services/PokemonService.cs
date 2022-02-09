using PokeDex.Api.Application.Interfaces;
using PokeDex.Api.Domain.Exceptions;
using PokeDex.Api.Domain.Models.Pokemon;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PokeDex.Api.Application.Services
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