using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpCache
{
    public class MemoryCache<TKey, TValue> : ICache<TKey, TValue>
    {
        private readonly Dictionary<TKey, CacheItem<TValue>> _store;
        private readonly object _lock = new object();
        private readonly int _maxSize;

        public MemoryCache(int maxSize = 100)
        {
            _store = new Dictionary<TKey, CacheItem<TValue>>();
            _maxSize = maxSize;
        }

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _store.Count;
                }
            }
        }

        public void Set(TKey key, TValue value, TimeSpan? expiry = null)
        {
            lock (_lock)
            {
                if (_store.Count >= _maxSize)
                    Cleanup();

                _store[key] = new CacheItem<TValue>(value, expiry);
            }
        }

        public bool TryGet(TKey key, out TValue value)
        {
            lock (_lock)
            {
                if (_store.TryGetValue(key, out var item))
                {
                    if (!item.IsExpired())
                    {
                        item.LastAccessed = DateTime.UtcNow;
                        value = item.Value;
                        return true;
                    }

                    _store.Remove(key);
                }
            }

            value = default!;
            return false;
        }

        public void Remove(TKey key)
        {
            lock (_lock)
            {
                _store.Remove(key);
            }
        }

        private void Cleanup()
        {
            var oldest = _store
                .OrderBy(x => x.Value.LastAccessed)
                .FirstOrDefault();

            if (!oldest.Equals(default(KeyValuePair<TKey, CacheItem<TValue>>)))
                _store.Remove(oldest.Key);
        }
    }
}
