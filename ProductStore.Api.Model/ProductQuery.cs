using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Model
{
    /// <summary>
    /// Represents a class to get the product information.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductQuery : IRequest<ProductRead>
    {
        /// <summary>
        /// Id to search the product
        /// </summary>
        public Guid Id { get; set; }
    }
}
