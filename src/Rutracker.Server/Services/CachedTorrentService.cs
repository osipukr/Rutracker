using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Services
{
    public class CachedTorrentViewModelService : ITorrentService
    {
        private readonly IMemoryCache _cache;
        private readonly TorrentViewModelService _torrentViewModelService;

        private readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);

        private const string TorrentsTemplate = "torrents-{0}-{1}-{2}";
        private const string TorrentTemplate = "torrent-{0}";
        private const string TitlesTemplate = "titles-{0}";

        public CachedTorrentViewModelService(IMemoryCache cache, TorrentViewModelService torrentViewModelService)
        {
            _cache = cache;
            _torrentViewModelService = torrentViewModelService;
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var cacheKey = string.Format(TorrentsTemplate, page, pageSize, filter.GetHashCode());

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);
            });
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            var cacheKey = string.Format(TorrentTemplate, id);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentIndexAsync(id);
            });
        }

        public async Task<FacetItemViewModel[]> GetTitlesAsync(int count)
        {
            var cacheKey = string.Format(TitlesTemplate, count);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTitlesAsync(count);
            });
        }
    }
}