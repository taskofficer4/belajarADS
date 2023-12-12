using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Actris.Infrastructure.Services
{
    /// <summary>
    /// In memory cache
    /// </summary>
    public class CacheService
    {

        private readonly CacheItemPolicy _defaultCacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) };

        private static Dictionary<string, CacheService> _all = new Dictionary<string, CacheService>();

        private MemoryCache _cache;
        private string _cacheCode;

        private CacheService()
        { }

        public static void ClearAllCache()
        {
            foreach (var cacheService in _all)
                cacheService.Value.Clear();
        }

        public static void ClearCache(string cacheInstanceCode)
        {
            if (_all.ContainsKey(cacheInstanceCode))
                _all[cacheInstanceCode].Clear();
        }

        public static CacheService GetInstance(string cacheInstanceCode)
        {
            if (_all.ContainsKey(cacheInstanceCode))
                return _all[cacheInstanceCode];

            var cs = new CacheService();
            cs._cacheCode = cacheInstanceCode;
            cs._cache = new MemoryCache(cacheInstanceCode);

            _all.Add(cacheInstanceCode, cs);
            return cs;
        }

        public async Task<T> GetFromCacheAsync<T>(string prefix, string itemKey, Func<string, Task<T>> adder, CacheItemPolicy policy = null)
        {
            if (string.IsNullOrEmpty(itemKey))
                return default(T);

            var cacheKey = $"{prefix}:{itemKey}";

            if (_cache.Contains(cacheKey))
                return (T)_cache[cacheKey];

            var value = await adder(itemKey);
            if (value == null)
                return default(T);

            if (policy == null)
                policy = _defaultCacheItemPolicy;

            _cache.Add(cacheKey, value, policy);

            return value;
        }

        private string Key(string prefix, string itemKey)
        {
            return $"{prefix}:{itemKey}";
        }

        public bool Exist(string prefix, string itemKey)
        {
            return _cache.Contains(Key(prefix, itemKey));
        }

        public T Get<T>(string prefix, string itemKey)
        {
            return (T)_cache[Key(prefix, itemKey)];
        }

        public void Add<T>(string prefix, string itemKey, T value, CacheItemPolicy policy = null)
        {
            if (policy == null)
                policy = _defaultCacheItemPolicy;

            if (value != null)
            {
                _cache.Add(Key(prefix, itemKey), value, policy);
            }
           
        }

        public T GetFromCache<T>(string prefix, string itemKey, Func<string, T> adder, CacheItemPolicy policy = null)
        {
            if (string.IsNullOrEmpty(itemKey))
                return default(T);

            if (Exist(prefix, itemKey))
                return Get<T>(prefix, itemKey);

            var value = adder(itemKey);
            if (value == null)
                return default(T);

            Add(prefix, itemKey, value, policy);
            return value;
        }

        public static Dictionary<string, IEnumerable<Tuple<string, object>>> GetAllCacheValue()
        {
            var result = new Dictionary<string, IEnumerable<Tuple<string, object>>>();
            foreach (var instance in _all)
                result.Add(instance.Key, instance.Value._cache.Select(o => new Tuple<string, object>(o.Key, o.Value)));

            return result;
        }

        public void Clear()
        {
            _cache.Dispose();
            _cache = new MemoryCache(_cacheCode);
        }

        public void Clear(string prefix, string itemKey)
        {
            _cache.Remove(Key(prefix, itemKey));
        }
    }
}