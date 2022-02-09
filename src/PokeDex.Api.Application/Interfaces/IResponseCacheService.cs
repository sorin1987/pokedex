using PokeDex.Api.Contracts.Responses;
using System;
using System.Threading.Tasks;

namespace PokeDex.Api.Application.Interfaces
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<PokemonResponse> GetCachedResponseASync(string cacheKey);
    }
}