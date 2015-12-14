using Microsoft.Extensions.Caching.Memory;

namespace BestFor.Services.Cache
{
    public class CacheManager
    {

        public CacheManager(IMemoryCache cache)
        {
        }

        private static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public static void Add(string key, object value)
        {
            _cache.Set(key, value);
        }

        public static object Get(string key)
        {
            object value;
            if (_cache.TryGetValue(key, out value))
                return value;
            return null;
        }
    }
}
