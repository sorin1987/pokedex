using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PokeDex.Api.Services;
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

        public static IServiceCollection AddApiHttpClient(this IServiceCollection services, IConfiguration configuration, string settingsSectionName, string clientName)
        {
            var random = new Random();
            var apiClientSettings = new ApiClientSettings();
            configuration.GetSection(settingsSectionName).Bind(apiClientSettings);
            services.AddHttpClient(clientName, client => { client.BaseAddress = new Uri(apiClientSettings.BaseAddress); })
                .AddTransientHttpErrorPolicy(x =>
                    x.WaitAndRetryAsync(apiClientSettings.RetrySettings.MaxRetries,
                        times => TimeSpan.FromSeconds(Math.Pow(2, times)) + TimeSpan.FromMilliseconds(random.Next(0,1000))));
            
            return services;
        }

        public static IServiceCollection AddCacheRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);

            if (redisCacheSettings.Enabled)
            {
                services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
                services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            }

            return services;
        }
    }
}