using System;

namespace SharpCache
{
    class Program
    {
        static void Main(string[] args)
        {
            var cache = new MemoryCache<string, string>(maxSize: 3);

            cache.Set("user:1", "Alex", TimeSpan.FromSeconds(10));
            cache.Set("user:2", "Ivan");
            cache.Set("user:3", "Sergey");

            if (cache.TryGet("user:1", out var name))
                Console.WriteLine($"Found: {name}");

            cache.Set("user:4", "Mikhail");

            Console.WriteLine($"Cache size: {cache.Count}");
        }
    }
}
