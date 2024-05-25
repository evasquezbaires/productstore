using Microsoft.EntityFrameworkCore;
using ProductStore.Api.Domain.Contracts;
using ProductStore.Api.Domain.Entities;

namespace ProductStore.Api.Repository
{
    /// <summary>
    /// Implements interface for the repository working with the Product entity
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly Lazy<DbSet<Product>> _dbSet;
        private readonly DbContext _dbContext;

        public ProductRepository(DatabaseContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbSet = new Lazy<DbSet<Product>>(() => dbContext.Set<Product>());
            _dbContext = dbContext;
        }

        public async Task<Product> AddProduct(Product entity, CancellationToken cancellationToken = default)
        {
            var added = await _dbSet.Value.AddAsync(entity, cancellationToken);
            return added.Entity;
        }

        public async Task AddProducts(IEnumerable<Product> entities, CancellationToken cancellationToken = default) => await _dbSet.Value.AddRangeAsync(entities, cancellationToken);

        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.Value.AnyAsync(x => x.Id.Equals(id));
        }

        public async Task<Product> Get(Guid id)
        {
            return await _dbSet.Value.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbSet.Value.ToListAsync();
        }

        public void Remove(Product entity) => _dbSet.Value.Remove(entity);
    }
}
