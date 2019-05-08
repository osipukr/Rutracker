using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Rutracker.Shared.Interfaces;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Shared.Services
{
    public class CachedTorrentViewModelService : ITorrentsViewModelService
    {
        private readonly IMemoryCache _cache;
        private readonly TorrentsViewModelService _torrentViewModelService;

        private readonly string _totalCountTemplate = "count-{0}";
        private readonly string _torrentsTemplate = "torrents-{0}-{1}-{2}-{3}-{4}";
        private readonly string _torrentDetailsTemplate = "torrent-details-{0}";
        private readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);

        public CachedTorrentViewModelService(IMemoryCache cache, TorrentsViewModelService torrentViewModelService)
        {
            _cache = cache;
            _torrentViewModelService = torrentViewModelService;
        }

        public async Task<int> GetTotalCountAsync(string search)
        {
            var cacheKey = string.Format(_totalCountTemplate, search);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTotalCountAsync(search);
            });
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(int pageIndex, int itemsPage, string search, string sort, string order)
        {
            var cacheKey = string.Format(_torrentsTemplate, pageIndex, itemsPage, search, sort, order);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentsAsync(pageIndex, itemsPage, search, sort, order);
            });
        }

        public async Task<TorrentDetailsViewModel> GetTorrentAsync(long torrentId)
        {
            var cacheKey = string.Format(_torrentDetailsTemplate, torrentId);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentAsync(torrentId);
            });
        }
    }
}