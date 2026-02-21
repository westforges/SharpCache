using System;

namespace SharpCache
{
    public interface ICache<TKey, TValue>
    {
        void Set(TKey key, TValue value, TimeSpan? expiry = null);
        bool TryGet(TKey key, out TValue value);
        void Remove(TKey key);
        int Count { get; }
    }
}
