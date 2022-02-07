using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using PokeDex.Api.Settings;
using PokeDex.Api.Utils;

namespace PokeDex.Api.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var version = ReflectionUtils.GetAssemblyVersion<Startup>();
            var applicationName = Program.ApplicationName;

            var swaggerSettings = new SwaggerSettings();
            configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerSettings.UiEndpoint, $"{applicationName} {version}");
            });

            return app;
        }
    }
}