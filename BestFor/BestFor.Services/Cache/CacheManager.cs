using Microsoft.Extensions.Caching.Memory;
using System;

namespace BestFor.Services.Cache
{
    public class CacheManager : ICacheManager
    {
        private IMemoryCache _cache;

        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

       // private static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public object Add(string key, object value)
        {
            _cache.Set(key, value);
            return value;
        }

        public object Add(string key, object value, int seconds)
        {
            // todo: redo this with injection that allows specifying time.Now
            var options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(seconds);
            _cache.Set(key, value, options);
            return value;
        }

        public object Get(string key)
        {
            object value;
            if (_cache.TryGetValue(key, out value))
                return value;
            return null;
        }
    }
}
