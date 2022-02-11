using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PokeDex.Api.Application.Settings;

namespace PokeDex.Api.Middleware
{
    public class ApiKeyMiddleware
    {
        private const string ApiKeyHeaderName = "ApiKey";

        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, ApiKeySettings apiKeySettings)
        {
            _next = next;
            _apiKey = apiKeySettings.ApiKey;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var providedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Api Key is missing");
                return;
            }

            if (_apiKey != providedApiKey)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next.Invoke(context);
        }
    }
}