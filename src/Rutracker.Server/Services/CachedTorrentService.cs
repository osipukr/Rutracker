using System;
using System.Collections.Generic;
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

        private readonly string _torrentsTemplate = "torrents-{0}-{1}-{2}";
        private readonly string _torrentTemplate = "torrent-{0}";
        private readonly string _titlesTemplate = "titles-{0}";
        private readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);

        public CachedTorrentViewModelService(IMemoryCache cache, TorrentViewModelService torrentViewModelService)
        {
            _cache = cache;
            _torrentViewModelService = torrentViewModelService;
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var cacheKey = string.Format(_torrentsTemplate, page, pageSize, filter.GetHashCode());

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, filter);
            });
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            var cacheKey = string.Format(_torrentTemplate, id);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentIndexAsync(id);
            });
        }

        public async Task<IEnumerable<FacetItem>> GetTitlesAsync(int count)
        {
            var cacheKey = string.Format(_titlesTemplate, count);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTitlesAsync(count);
            });
        }
    }
}