namespace ProductStore.Api.Domain.Contracts
{
    /// <summary>
    /// Definition to manage the cache.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Retrieve a value from the cache.
        /// </summary>
        /// <param name="key">Key used to find the value.</param>
        /// <returns>The result of the asynchronous operation.</returns>
        Task<string> Get(string key);

        /// <summary>
        /// Save a new item on the cache.
        /// </summary>
        /// <param name="key">Key used as identifier.</param>
        /// <param name="value">Value to be saved.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task Save(string key, string value);

        /// <summary>
        /// Save a new item on the cache if not exists, in other case return its value.
        /// </summary>
        /// <param name="key">Key used as identifier.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task<string> CheckCache(string key);
    }
}
