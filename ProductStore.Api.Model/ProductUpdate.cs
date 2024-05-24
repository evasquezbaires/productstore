using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Model
{
    /// <summary>
    /// Represents a class to update the product information.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductUpdate : ProductWrite
    {
        /// <summary>
        /// Id for the product to update
        /// </summary>
        public Guid Id { get; set; }
    }
}
