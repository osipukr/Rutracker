using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Services
{
    public class CachedTorrentViewModelService : ITorrentService
    {
        private readonly IMemoryCache _cache;
        private readonly TorrentService _torrentViewModelService;

        private readonly string _torrentsTemplate = "torrents-{0}-{1}-{2}";
        private readonly string _detailsTemplate = "details-{0}";
        private readonly string _forumsTemplate = "forums-{0}";
        private readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);

        public CachedTorrentViewModelService(IMemoryCache cache, TorrentService torrentViewModelService)
        {
            _cache = cache;
            _torrentViewModelService = torrentViewModelService;
        }

        public async Task<TorrentsViewModel> GetTorrentsAsync(int pageIndex, int itemsPage, string search)
        {
            var cacheKey = string.Format(_torrentsTemplate, pageIndex, itemsPage, search);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentsAsync(pageIndex, itemsPage, search);
            });
        }

        public async Task<DetailsViewModel> GetTorrentAsync(long torrentId)
        {
            var cacheKey = string.Format(_detailsTemplate, torrentId);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetTorrentAsync(torrentId);
            });
        }

        public async Task<FiltrationViewModel> GetFiltrationAsync(int count)
        {
            var cacheKey = string.Format(_forumsTemplate, count);

            return await _cache.GetOrCreateAsync(cacheKey,  async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                return await _torrentViewModelService.GetFiltrationAsync(count);
            });
        }
    }
}