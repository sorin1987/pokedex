using FluentAssertions;
using Moq;
using Moq.Protected;
using PokeDex.Api.Application.Interfaces;
using PokeDex.Api.Application.Services;
using PokeDex.Api.Application.Settings;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PokeDex.Api.Application;
using Xunit;

namespace PokeDex.Api.UnitTests.Services
{
    public class TranslationServiceUnitTests
    {
        private ITranslationService _sut;
        private readonly Mock<IHttpClientFactory> _httpFactory;
        private readonly CircuitBreakerSettings _settings;

        public TranslationServiceUnitTests()
        {
            _settings = new CircuitBreakerSettings();
            _httpFactory = new Mock<IHttpClientFactory>();
        }
        
        [Fact]
        public async Task TranslateTextAsync_ShouldReturnTranslatedText_WhenTranslationsApiIsAvailable()
        {
            _sut = new TranslationService(_httpFactory.Object, new RetryPolicies(), _settings);
            var httpClient = CreateHttpClientMock(HttpStatusCode.OK);
            _httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var result = await _sut.TranslateTextAsync("test text", "sorin", default);

            result.Should().NotBeNull();
            result.Should().Be("this is a translated text");
        }
        
        [Fact]
        public async Task TranslateTextAsync_ShouldReturnOriginalText_WhenTranslationsApiIsThrottling()
        {
            _sut = new TranslationService(_httpFactory.Object, new RetryPolicies(), _settings);
            var httpClient = CreateHttpClientMock(HttpStatusCode.TooManyRequests);
            _httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var result = await _sut.TranslateTextAsync("original text with no translation", "sorin", default);

            result.Should().NotBeNull();
            result.Should().Be("original text with no translation");
        }
        
        private HttpClient CreateHttpClientMock(HttpStatusCode returnedCode)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = returnedCode,
                    Content = new StringContent("{\"success\":{\"total\":1},\"contents\":{\"translated\":\"this is a translated text\",\"text\":\"text in the original form\",\"translation\":\"sorin\"}}")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://test.com");
            return client;
        }
    }
}