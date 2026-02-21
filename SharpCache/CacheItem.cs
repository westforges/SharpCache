using System;

namespace SharpCache
{
    internal class CacheItem<T>
    {
        public T Value { get; }
        public DateTime? Expiry { get; }
        public DateTime LastAccessed { get; set; }

        public CacheItem(T value, TimeSpan? ttl)
        {
            Value = value;
            Expiry = ttl.HasValue ? DateTime.UtcNow.Add(ttl.Value) : null;
            LastAccessed = DateTime.UtcNow;
        }

        public bool IsExpired()
        {
            return Expiry.HasValue && DateTime.UtcNow > Expiry.Value;
        }
    }
}
