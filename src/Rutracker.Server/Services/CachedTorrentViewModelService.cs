using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Services.Types;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Services
{
    public class CachedTorrentViewModelService : ITorrentViewModelService
    {
        private static class KeyTemplate
        {
            public const string TorrentsIndex = "torrents-{0}-{1}-{2}";
            public const string TorrentIndex = "torrent-{0}";
            public const string Titles = "titles-{0}";
        }

        private readonly IMemoryCache _cache;
        private readonly ITorrentViewModelService _torrentViewModelService;
        private readonly TimeSpan _defaultCacheDuration;

        public CachedTorrentViewModelService(IMemoryCache cache,
            Func<TorrentViewModelServiceType, ITorrentViewModelService> serviceResolver)
        {
            _cache = cache;
            _torrentViewModelService = serviceResolver(TorrentViewModelServiceType.Default);
            _defaultCacheDuration = TimeSpan.FromSeconds(30);
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var cacheKey = string.Format(KeyTemplate.TorrentsIndex, page, pageSize, filter?.GetHashCode());

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);
            });
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            var cacheKey = string.Format(KeyTemplate.TorrentIndex, id);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentIndexAsync(id);
            });
        }

        public async Task<FacetItemViewModel[]> GetTitlesAsync(int count)
        {
            var cacheKey = string.Format(KeyTemplate.Titles, count);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTitlesAsync(count);
            });
        }
    }
}