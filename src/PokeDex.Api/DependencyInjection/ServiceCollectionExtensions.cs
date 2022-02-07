using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PokeDex.Api.Settings;
using PokeDex.Api.Utils;
using Polly;

namespace PokeDex.Api.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services, string title)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = title,
                    Version = ReflectionUtils.GetAssemblyVersion<Program>()
                });
            });
            return services;
        }

        public static IServiceCollection AddPokemonApiHttpClient(this IServiceCollection services, PokemonApiSettings settings)
        {
            services.AddHttpClient("PokemonApi",
                    client => { client.BaseAddress = new Uri(settings.BaseAddress); })
                .AddTransientHttpErrorPolicy(x =>
                    x.WaitAndRetryAsync(settings.RetrySettings.MaxRetries,
                        times => TimeSpan.FromMilliseconds(times * settings.RetrySettings.WaitBetweenRetriesFactorMilliseconds)));
            
            return services;
        }
    }
}