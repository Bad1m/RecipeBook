using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using ReviewMicroservice.Domain.Settings;
using ReviewMicroservice.Infrastructure.Interfaces;
using System.Text.Json;

namespace ReviewMicroservice.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _cache;
        private readonly CacheOptions _cacheOptions;

        public CacheRepository(IDistributedCache cache, IOptions<CacheOptions> cacheOptions)
        {
            _cache = cache;
            _cacheOptions = cacheOptions.Value;
        }

        public async Task<T> GetDataAsync<T>(string cacheKey)
        {
            var data = await _cache.GetAsync(cacheKey);

            if (data == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task SetDataAsync<T>(string cacheKey, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpiration,
                SlidingExpiration = _cacheOptions.SlidingExpiration
            };

            var jsonData = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(cacheKey, jsonData, options);
        }

        public async Task RemoveAsync(string cacheKey)
        {
            await _cache.RemoveAsync(cacheKey);
        }
    }
}