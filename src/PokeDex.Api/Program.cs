using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PokeDex.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        public static string ApplicationName =>
            Environment.GetEnvironmentVariable("DOTNET_APPLICATIONNAME") ??
            Environment.GetEnvironmentVariable("ASPNETCORE_APPLICATIONNAME") ??
            "PokeDex API";
    }
}
