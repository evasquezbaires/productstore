using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using ProductStore.Api.Client;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ProductStore.Api.Test.Client
{
    [ExcludeFromCodeCoverage]
    public class DiscountClientServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IOptions<DiscountClientConfiguration>> _configMock;
        private readonly DiscountClientService _discountClientService;

        public DiscountClientServiceTests()
        {
            // Set up the configuration
            var config = new DiscountClientConfiguration
            {
                BaseUrl = "https://api.example.com",
                Endpoint = "discounts"
            };
            _configMock = new Mock<IOptions<DiscountClientConfiguration>>();
            _configMock.Setup(c => c.Value).Returns(config);

            // Set up the HttpClient mock
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken cancellationToken) =>
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{\"value\": 50}")
                    };
                    return response;
                });

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri(config.BaseUrl)
            };

            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpClientFactoryMock.Setup(cf => cf.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Initialize the service
            _discountClientService = new DiscountClientService(_httpClientFactoryMock.Object, _configMock.Object);
        }

        [Fact]
        public async Task GetDiscount_ReturnsExpectedDiscount()
        {
            // Arrange
            var discountId = Guid.NewGuid();

            // Act
            var discount = await _discountClientService.GetDiscount(discountId);

            // Assert
            Assert.Equal(50, discount);
        }
    }
}
