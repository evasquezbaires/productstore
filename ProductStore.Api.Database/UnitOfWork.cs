using ProductStore.Api.Domain.Contracts;

namespace ProductStore.Api.Repository
{
    /// <summary>
    /// Implements Unit of work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dbContext;

        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task Rollback()
        {
            _dbContext.ChangeTracker.Clear();
            await Task.CompletedTask;
        }
    }
}
