using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace PokeDex.Api.UnitTests.Mappers
{
    public class AutoMapperUnitTests
    {
        [Fact]
        public void AddAutoMapper_ShouldRegisterAllTheMappingProfiles_WhenTargetedThroughATypeFlag()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(typeof(Startup));
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var mapper = serviceProvider.GetRequiredService<IMapper>();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}