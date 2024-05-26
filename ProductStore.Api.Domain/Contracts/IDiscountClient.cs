namespace ProductStore.Api.Domain.Contracts
{
    /// <summary>
    /// Interface to define communication with external service for the Discount
    /// </summary>
    public interface IDiscountClient
    {
        /// <summary>
        /// Gets the discount by external service invocation
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the percentage of discount</returns>
        Task<int> GetDiscount(Guid id);
    }
}
