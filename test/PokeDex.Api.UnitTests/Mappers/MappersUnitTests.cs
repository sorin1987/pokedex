using AutoFixture;
using AutoMapper;
using FluentAssertions;
using PokeDex.Api.Contracts.Responses;
using PokeDex.Api.Domain.Models.Pokemon;
using PokeDex.Api.Mapping;
using System.Collections.Generic;
using Xunit;

namespace PokeDex.Api.UnitTests.Mappers
{
    public class MappersUnitTests
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;

        public MappersUnitTests()
        {
            _fixture = new Fixture();
            var config = new MapperConfiguration(x => { x.AddProfile<DomainToResponseProfile>(); });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Map_PokemonToPokemonResponse_ShouldBeSuccessful()
        {
            var pokemon = _fixture.Build<Pokemon>()
                .With(x => x.Descriptions, new List<Description>
                {
                    new Description { Language = new Language { Name = "fr" }, Text = "French text description" },
                    new Description { Language = new Language { Name = "ro" }, Text = "Romanian text description" },
                    new Description { Language = new Language { Name = "en" }, Text = "English text description" }
                })
                .Create();

            var response = _mapper.Map<PokemonResponse>(pokemon);

            response.Name.Should().Be(pokemon.Name);
            response.Habitat.Should().Be(pokemon.Habitat.Name);
            response.IsLegendary.Should().Be(pokemon.IsLegendary);
            response.Description.Should().Be("English text description");
        }
    }
}