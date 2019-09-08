using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Rutracker.Server.BusinessLayer.Extensions
{
    public static class MemoryCacheExtensions
    {
        public static async Task<TItem> GetOrCreateAsync<TItem>(this IMemoryCache cache,
            object key,
            Func<Task<TItem>> func,
            MemoryCacheEntryOptions options)
        {
            return await cache.GetOrCreateAsync(key, async entry =>
            {
                entry.SetOptions(options);

                return await func();
            });
        }
    }
}