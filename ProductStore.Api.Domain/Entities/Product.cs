using ProductStore.Api.Model.Enum;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Domain.Entities
{
    /// <summary>
    /// Represents the Entity of the product.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Product : BaseEntity
    {
        /// <summary>
        /// Barcode or internal identification for the product
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The visual name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The active quantity of the product
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// The product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Defined base price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The current status of the product
        /// </summary>
        public int StatusCode { get; set; }
    }
}
