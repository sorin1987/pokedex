using System;
using System.Net.Http;
using Polly;
using Polly.CircuitBreaker;

namespace PokeDex.Api
{
    public class RetryPolicies
    {
        public AsyncCircuitBreakerPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(int retriesBeforeBreaking, TimeSpan breakDuration)
        {
            var policy = Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode == 429)
                .CircuitBreakerAsync(retriesBeforeBreaking, breakDuration);
            return policy;
        }
    }
}