using ProductStore.Api.Domain.Entities;

namespace ProductStore.Api.Domain.Contracts
{
    /// <summary>
    /// Represents interface for the repository working with the Product entity
    /// </summary>
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product entity, CancellationToken cancellationToken = default);

        Task AddProducts(IEnumerable<Product> entities, CancellationToken cancellationToken = default);

        Task<bool> Exists(Guid id);

        Task<Product> Get(Guid id);

        Task<IEnumerable<Product>> GetAll();

        void Remove(Product entity);
    }
}
