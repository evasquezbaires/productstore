using LazyCache;

namespace ProductStore.Api.Configuration
{
    /// <summary>
    /// Custom configuration for Cache injection
    /// </summary>
    public static class CacheConfiguration
    {
        // <summary>
        /// Allows to configure the necessary dependencies for Cache service
        /// </summary>
        /// <param name="services">Services of the application</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <returns>Services with added configuration</returns>
        public static void ConfigureCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLazyCache(serviceProvider => {
                var cache = new CachingService(CachingService.DefaultCacheProvider);
                cache.DefaultCachePolicy.DefaultCacheDurationSeconds = int.Parse(configuration["CacheService:Expiration"]);
                return cache;
            });
        }
    }
}
