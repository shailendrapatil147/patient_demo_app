using Demo.API.Common.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Demo.API.Common.Caching
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;

        private const int SLIDING_EXPIRATION_DEFAULT = 60; // default to 1 hour if not configured
        private const int ABSOLUTE_EXPIRATION_DEFAULT = 320; // default to 4 hour if not configured

        public CacheProvider(IConfiguration configuration,
                            IDistributedCache cache,
                            ILoggingService loggingService)
        {
            _logger = loggingService.GetLogger<CacheProvider>(nameof(CacheProvider));
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<T> GetData<T>(string path, string identifier, Func<Task<T>> GetFromDatabse) where T : class
        {
            string key = GetCacheKey(path, identifier);

            _logger.LogDebug($"Trying to fecth {key} from  cache");

            T cacheObj = await GetFromCache<T>(key);

            if (cacheObj == null)
            {
                if (GetFromDatabse != null)
                {
                    _logger?.LogDebug($"Making call to db as key was not found");

                    cacheObj = await GetFromDatabse();

                    if (cacheObj != null)
                    {
                        _logger?.LogDebug($"Set result in  cache");

                        await SetToCache(path, identifier, cacheObj);

                        return cacheObj;
                    }
                }
            }

            return cacheObj;
        }

        private string GetCacheKey(string path, string identifier)
        {
            return $"{_configuration[$"{path}:Key"]}_{identifier}";
        }

        private async Task SetToCache<T>(string path, string identifier, T cacheObj) where T : class
        {
            byte[] arr = DistributedCacheSerializer.Serialize(cacheObj);

            (int slidingExpirationInMinutes, int absoluteExpirationInMinutes) = GetCacheSettings(path);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpirationInMinutes),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(absoluteExpirationInMinutes)
            };

            string key = GetCacheKey(path, identifier);

            await _cache.SetAsync(key, arr, cacheOptions).ConfigureAwait(false);
        }

        private (int, int) GetCacheSettings(string path)
        {
            int slidingExpirationInMinutes = SLIDING_EXPIRATION_DEFAULT;
            int absoluteExpirationInMinutes = ABSOLUTE_EXPIRATION_DEFAULT;

            int.TryParse(_configuration[$"{path}:SlidingExpiration"], out slidingExpirationInMinutes);
            int.TryParse(_configuration[$"{path}:AbsoluteExpiration"], out absoluteExpirationInMinutes);

            return (slidingExpirationInMinutes, absoluteExpirationInMinutes);
        }

        private async Task<T> GetFromCache<T>(string key) where T : class
        {
            T cacheObj = null;
            var byteArray = await _cache.GetAsync(key);

            if (byteArray != null)
            {
                cacheObj = await DistributedCacheSerializer.Deserialize<T>(byteArray).ConfigureAwait(false);
            }

            return cacheObj;
        }

        public async void Remove(string path, string identifier)
        {
            var key = GetCacheKey(path, identifier);
            await _cache.RemoveAsync(key);
        }
    }
}
