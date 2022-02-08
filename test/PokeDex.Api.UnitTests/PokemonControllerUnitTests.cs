using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PokeDex.Api.Controllers;
using PokeDex.Api.Domain.Pokemon;
using PokeDex.Api.Exceptions;
using PokeDex.Api.Responses;
using PokeDex.Api.Services;
using PokeDex.Api.TranslationProviders;
using Xunit;

namespace PokeDex.Api.UnitTests
{
    public class PokemonControllerUnitTests
    {
        private PokemonController _sut;
        private readonly Mock<IPokemonService> _pokemonService;
        private readonly Mock<ITranslationProviderFactory> _translationProviderFactory;
        private readonly Mock<IMapper> _mapper;

        public PokemonControllerUnitTests()
        {
            _pokemonService = new Mock<IPokemonService>();
            _translationProviderFactory = new Mock<ITranslationProviderFactory>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetPokemonAsync_ShouldReturnOK_WhenPokemonExistsAndPokemonApiReachable()
        {
            _pokemonService.Setup(x => x.GetPokemonByNameAsync(It.IsAny<string>(), default))
                .ReturnsAsync(new Pokemon());
            _mapper.Setup(x => x.Map<Pokemon, PokemonResponse>(It.IsAny<Pokemon>())).Returns(new PokemonResponse());

            _sut = new PokemonController(_pokemonService.Object, _mapper.Object, _translationProviderFactory.Object);

            var result = await _sut.GetPokemonAsync("pikachu", default);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public async Task GetPokemonAsync_ShouldReturnNotFound_WhenPokemonApiThrowsAnApiException()
        {
            _pokemonService.Setup(x => x.GetPokemonByNameAsync(It.IsAny<string>(), default))
                .ThrowsAsync(new PokemonApiException());

            _sut = new PokemonController(_pokemonService.Object, _mapper.Object, _translationProviderFactory.Object);

            var result = await _sut.GetPokemonAsync("pikachu", default);

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
        
        [Fact]
        public async Task GetTranslatedPokemonAsync_ShouldReturnOK_WhenPokemonExistsAndPokemonApiReachable()
        {
            _pokemonService.Setup(x => x.GetPokemonByNameAsync(It.IsAny<string>(), default))
                .ReturnsAsync(new Pokemon());
            _mapper.Setup(x => x.Map<Pokemon, PokemonResponse>(It.IsAny<Pokemon>())).Returns(new PokemonResponse());
            var translationProvider = new YodaTranslationProvider();
            _translationProviderFactory.Setup(x => x.GetTranslationProvider(It.IsAny<PokemonResponse>()))
                .Returns(translationProvider);
            _translationProviderFactory.Setup(x => x.ApplyTranslationAsync(It.IsAny<ITranslationProvider>(), "", default))
                .ReturnsAsync("translated string");

            _sut = new PokemonController(_pokemonService.Object, _mapper.Object, _translationProviderFactory.Object);

            var result = await _sut.GetPokemonAsync("pikachu", default);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public async Task GetTranslatedPokemonAsync_ShouldReturnNotFound_WhenAnApiExceptionIsThrown()
        {
            _pokemonService.Setup(x => x.GetPokemonByNameAsync(It.IsAny<string>(), default))
                .ThrowsAsync(new PokemonApiException());

            _sut = new PokemonController(_pokemonService.Object, _mapper.Object, _translationProviderFactory.Object);

            var result = await _sut.GetTranslatedPokemonAsync("pikachu", default);

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}