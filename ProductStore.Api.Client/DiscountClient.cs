using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using ProductStore.Api.Domain.Contracts;

namespace ProductStore.Api.Client
{
    /// <summary>
    /// Implements communication with external service for the Discount
    /// </summary>
    public class DiscountClient : IDiscountClient
    {
        private readonly HttpClient _client;
        private readonly DiscountClientConfiguration _clientConfig;

        public DiscountClient(IHttpClientFactory clientFactory, IOptions<DiscountClientConfiguration> configuration) 
        {
            _client = clientFactory.CreateClient();
            _clientConfig = configuration.Value;
            _client.BaseAddress = new Uri(_clientConfig.BaseUrl);
        }

        public async Task<int> GetDiscount(Guid id)
        {
            var response = await _client.GetAsync($"{_clientConfig.Endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var discount = JObject.Parse(responseBody)["value"].Value<int>();
            return discount;
        }
    }
}
