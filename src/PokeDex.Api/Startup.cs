using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PokeDex.Api.DependencyInjection;
using PokeDex.Api.Services;
using PokeDex.Api.Settings;
using Polly;
using Swashbuckle;

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

            var pokemonApiSettings = new PokemonApiSettings();
            _configuration.GetSection(nameof(PokemonApiSettings)).Bind(pokemonApiSettings);
            services.AddPokemonApiHttpClient(pokemonApiSettings);

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<IPokemonService, PokemonService>();
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