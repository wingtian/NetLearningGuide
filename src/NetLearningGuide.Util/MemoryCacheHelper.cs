using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace NetLearningGuide.Util
{
    public static class MemoryCacheHelper
    {
        public static async Task<T> UseCache<T>(this IMemoryCache cache, Func<string> cacheKeyGenerator,
            Func<Task<T>> domainLogic, TimeSpan? expiryTime = null)
        {
            var cacheKey = cacheKeyGenerator();
            if (cache.TryGetValue(cacheKey, out T cacheResult))
            {
                return cacheResult;
            }
            var cacheTime = expiryTime ?? TimeSpan.FromMinutes(5);
            var response = await domainLogic();
            if (response != null)
            {
                cache.Set(cacheKey, response, cacheTime);
            }
            return response;
        }

        public static T UseCache<T>(this IMemoryCache cache, Func<string> cacheKeyGenerator,
            Func<T> domainLogic, TimeSpan? expiryTime = null)
        {
            var cacheKey = cacheKeyGenerator();
            if (cache.TryGetValue(cacheKey, out T cacheResult))
            {
                return cacheResult;
            }

            var cacheTime = expiryTime ?? TimeSpan.FromMinutes(5);
            var response = domainLogic();
            if (response != null)
            {
                cache.Set(cacheKey, response, cacheTime);
            }
            return response;
        }


    }
}
