using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokeDex.Api.Application;
using PokeDex.Api.Application.Interfaces;
using PokeDex.Api.Application.Services;
using PokeDex.Api.Application.Settings;
using PokeDex.Api.Application.TranslationProviders;
using PokeDex.Api.DependencyInjection;

namespace PokeDex.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.RegisterSwagger(Program.ApplicationName);

            services.AddApiHttpClient(_configuration, "PokemonApiSettings", "PokemonApi");
            services.AddApiHttpClient(_configuration, "FunTranslationsApiSettings", "TranslationsApi");

            services.AddAutoMapper(typeof(Startup));

            services.AddCacheRegistration(_configuration);

            services.AddSingleton<IPokemonService, PokemonService>();
            services.AddSingleton<ITranslationService, TranslationService>();
            services.AddSingleton<RetryPolicies, RetryPolicies>();
            services.AddSingleton<CircuitBreakerSettings, CircuitBreakerSettings>();
            services.AddSingleton<ITranslationProviderFactory, TranslationProviderFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(_configuration);
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}