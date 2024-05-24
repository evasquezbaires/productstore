using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Model
{
    /// <summary>
    /// Represents a class to show the product information.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductRead : ProductBase
    {
        /// <summary>
        /// The current status of the product
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Applicable discount for the product
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// The final price including the discount
        /// </summary>
        public double FinalPrice { get; set; }
    }
}
