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
        private readonly TorrentService _torrentViewModelService;

        private readonly string _torrentsTemplate = "torrents-{0}-{1}-{2}";
        private readonly string _torrentTemplate = "torrent-{0}";
        private readonly string _titlesTemplate = "titles-{0}";
        private readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);

        public CachedTorrentViewModelService(IMemoryCache cache, TorrentService torrentViewModelService)
        {
            _cache = cache;
            _torrentViewModelService = torrentViewModelService;
        }

        public async Task<TorrentsViewModel> GetTorrentsIndexAsync(int page, int pageSize, string search)
        {
            var cacheKey = string.Format(_torrentsTemplate, page, pageSize, search);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentsIndexAsync(page, pageSize, search);
            });
        }

        public async Task<TorrentViewModel> GetTorrentIndexAsync(long id)
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