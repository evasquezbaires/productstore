namespace ProductStore.Api.Client
{
    /// <summary>
    /// Represents the configuration for the client Discount
    /// </summary>
    public class DiscountClientConfiguration
    {
        /// <summary>
        /// Base uri to invoke
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Endpoint to invoke
        /// </summary>
        public string Endpoint { get; set; }
    }
}
