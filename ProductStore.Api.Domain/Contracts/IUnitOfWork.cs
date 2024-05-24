namespace ProductStore.Api.Domain.Contracts
{
    /// <summary>
    /// IUnitOfWork.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Rollback the last operation.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Rollback();

        /// <summary>
        /// Commits the last operation.
        /// </summary>
        /// <returns>Transaction status.</returns>
        Task<int> CommitAsync();
    }
}
