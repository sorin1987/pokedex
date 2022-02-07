using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.Protected;
using PokeDex.Api.Exceptions;
using PokeDex.Api.Services;
using Xunit;

namespace PokeDex.Api.UnitTests
{
    public class PokemonServiceUnitTests
    {
        private IPokemonService _sut;
        private readonly Mock<IHttpClientFactory> _httpFactory;

        public PokemonServiceUnitTests()
        {
            _httpFactory = new Mock<IHttpClientFactory>();
        }

        [Fact]
        public async Task GetPokemonByNameAsync_ShouldReturnPokemon_WhenPokemonApiIsReachableAndValidDataIsProvided()
        {
            _sut = new PokemonService(_httpFactory.Object);
            var httpClient = CreateHttpClientMock(HttpStatusCode.OK);
            _httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var result = await _sut.GetPokemonByNameAsync("pikachu", default);

            result.Should().NotBeNull();
            result.Name.Should().Be("pikachu");
            result.Descriptions.Count().Should().Be(3);
            result.Habitat.Name.Should().Be("forest");
            result.IsLegendary.Should().BeFalse();
        }
        
        [Theory]
        [InlineData(HttpStatusCode.BadGateway)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task GetPokemonByNameAsync_ShouldThrowException_WhenPokemonApiIsReachableAndValidDataIsProvided(HttpStatusCode code)
        {
            _sut = new PokemonService(_httpFactory.Object);
            var httpClient = CreateHttpClientMock(code);
            _httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            await Assert.ThrowsAsync<PokemonApiException>(() => _sut.GetPokemonByNameAsync(It.IsAny<string>(), default));
        }

        private HttpClient CreateHttpClientMock(HttpStatusCode returnedCode)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = returnedCode,
                    Content = new StringContent("{\"flavor_text_entries\":[{\"flavor_text\":\"When several of these POKéMON gather, their electricity could build and cause lightning storms.\",\"language\":{\"name\":\"fr\",\"url\":\"https://pokeapi.co/api/v2/language/9/\"},\"version\":{\"name\":\"red\",\"url\":\"https://pokeapi.co/api/v2/version/1/\"}},{\"flavor_text\":\"When several of these POKéMON gather, their electricity could build and cause lightning storms.\",\"language\":{\"name\":\"ro\",\"url\":\"https://pokeapi.co/api/v2/language/9/\"},\"version\":{\"name\":\"blue\",\"url\":\"https://pokeapi.co/api/v2/version/2/\"}},{\"flavor_text\":\"Desription in English\",\"language\":{\"name\":\"en\",\"url\":\"https://pokeapi.co/api/v2/language/9/\"},\"version\":{\"name\":\"yellow\",\"url\":\"https://pokeapi.co/api/v2/version/3/\"}}],\"habitat\":{\"name\":\"forest\",\"url\":\"https://pokeapi.co/api/v2/pokemon-habitat/2/\"},\"name\":\"pikachu\",\"is_legendary\":false}")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://test.com");
            return client;
        }
    }
}