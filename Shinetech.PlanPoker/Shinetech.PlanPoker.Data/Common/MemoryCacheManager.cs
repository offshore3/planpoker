using System.Collections.Generic;

namespace Shinetech.PlanPoker.Data.Common
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly Dictionary<string, object> _cache;

        public MemoryCacheManager()
        {
            _cache = new Dictionary<string, object>();
        }

        public void Add(string key, object value)
        {
            _cache.Add(key, value);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public T Get<T>(string key)
        {
            return (T)_cache[key];
        }

        public bool KeyExist(string key)
        {
            return _cache.ContainsKey(key);
        }
    }
}
