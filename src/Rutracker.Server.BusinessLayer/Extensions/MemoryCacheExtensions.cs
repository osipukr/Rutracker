using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Rutracker.Server.BusinessLayer.Extensions
{
    public static class MemoryCacheExtensions
    {
        public static async Task<TItem> GetOrCreateAsync<TItem>(this IMemoryCache cache,
            object key,
            Task<TItem> factory,
            MemoryCacheEntryOptions options)
        {
            return await cache.GetOrCreateAsync(key, async entry =>
            {
                entry.SetOptions(options);

                return await factory;
            });
        }
    }
}