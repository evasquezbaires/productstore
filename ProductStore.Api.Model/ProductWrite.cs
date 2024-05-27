using MediatR;
using ProductStore.Api.Model.Enum;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Model
{
    /// <summary>
    /// Represents a class to write the product information.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductWrite : ProductBase, IRequest<ProductQuery>
    {
        /// <summary>
        /// The current status of the product
        /// </summary>
        public Status StatusCode { get; set; }
    }
}
