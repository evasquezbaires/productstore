using LazyCache;
using ProductStore.Api.Domain.Contracts;
using ProductStore.Api.Model.Enum;

namespace ProductStore.Api.Cache
{
    /// <summary>
    /// Implements cache interface to manage the Status cache.
    /// </summary>
    public class StatusCacheService : ICacheService        
    {
        private readonly IAppCache _cache;

        public StatusCacheService(IAppCache cache)
        {
            _cache = cache;
        }

        public async Task<string> Get(string key)
        {
            return await _cache.GetAsync<string>(key);
        }

        public Task Save(string key, string value)
        {
            _cache.Add<string>(key, value, null);

            return Task.CompletedTask;
        }

        public async Task<string> CheckCache(string key)
        {
            return await _cache.GetOrAddAsync<string>(key, () => InitCache(key));
        }

        private Task<string> InitCache(string key)
        {
            var value = Enum.Parse(typeof(Status), key);

            return Task.FromResult(value.ToString());
        }
    }
}
