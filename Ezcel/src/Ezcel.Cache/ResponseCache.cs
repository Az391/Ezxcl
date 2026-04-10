using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ezcel.Cache
{
    public class ResponseCache
    {
        private readonly IMemoryCache _cache;

        public ResponseCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string prompt, string model)
        {
            var key = GenerateCacheKey(prompt, model);
            if (_cache.TryGetValue(key, out T value))
            {
                return value;
            }
            return default;
        }

        public void Set<T>(string prompt, string model, T value)
        {
            var key = GenerateCacheKey(prompt, model);
            _cache.Set(key, value, new Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = System.TimeSpan.FromHours(1)
            });
        }

        public void Remove(string prompt, string model)
        {
            var key = GenerateCacheKey(prompt, model);
            _cache.Remove(key);
        }

        private string GenerateCacheKey(string prompt, string model)
        {
            using (var sha256 = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes($"{prompt}:{model}");
                var hashBytes = sha256.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}