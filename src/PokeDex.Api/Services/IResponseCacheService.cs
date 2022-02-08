using System;
using System.Threading.Tasks;
using PokeDex.Api.Responses;

namespace PokeDex.Api.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<PokemonResponse> GetCachedResponseASync(string cacheKey);
    }
}