using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Rutracker.Server.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<TItem> GetOrCreateAsync<TItem>(this IMemoryCache cache,
            object key,
            Func<Task<TItem>> callback,
            MemoryCacheEntryOptions options)
        {
            return await cache.GetOrCreateAsync(key, async entry =>
            {
                entry.SetOptions(options);

                return await callback();
            });
        }
    }
}