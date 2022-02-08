using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using PokeDex.Api.Domain.Translation;
using PokeDex.Api.Requests;
using PokeDex.Api.Settings;
using Polly.CircuitBreaker;

namespace PokeDex.Api.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy;

        public TranslationService(IHttpClientFactory httpClientFactory, RetryPolicies retryPolicies, CircuitBreakerSettings settings)
        {
            _httpClientFactory = httpClientFactory;
            _circuitBreakerPolicy = retryPolicies.GetCircuitBreakerPolicy(settings.RetriesBeforeBreaking, settings.BreakDuration);
        }

        public async Task<string> TranslateTextAsync(string text, string translationName, CancellationToken cancellationToken)
        {
            if (_circuitBreakerPolicy.CircuitState == CircuitState.Open)
            {
                return text;
            }

            var httpClient = _httpClientFactory.CreateClient("TranslationsApi");
            var json = JsonSerializer.Serialize(new TranslationRequest { Text = text });
            var stringContent = new StringContent(json.ToLower(), Encoding.UTF8, "application/json");

            var response = await _circuitBreakerPolicy.ExecuteAsync(
                () => httpClient.PostAsync($"{translationName}.json", stringContent, cancellationToken));

            if (!response.IsSuccessStatusCode)
            {
                return text;
            }

            var stream = await response.Content.ReadAsStreamAsync();
            var translation = await JsonSerializer.DeserializeAsync<FunTranslation>(stream, cancellationToken: cancellationToken);
            return translation.Content.Translated;
        }
    }
}