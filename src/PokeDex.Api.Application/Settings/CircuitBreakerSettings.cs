using System;

namespace PokeDex.Api.Application.Settings
{
    public class CircuitBreakerSettings
    {
        public int RetriesBeforeBreaking { get; set; } = 1;
        public TimeSpan BreakDuration { get; set; } = TimeSpan.FromMinutes(60);
    }
}