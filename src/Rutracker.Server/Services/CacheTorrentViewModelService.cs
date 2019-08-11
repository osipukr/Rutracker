using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Rutracker.Core.Interfaces.Services;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Settings;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Services
{
    public partial class TorrentViewModelService : ITorrentViewModelService
    {
        private readonly ITorrentService _torrentService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;

        public TorrentViewModelService(ITorrentService torrentService,
            IMapper mapper,
            IMemoryCache cache,
            IOptions<CacheSettings> cacheOptions)
        {
            _torrentService = torrentService ?? throw new ArgumentNullException(nameof(torrentService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

            if (cacheOptions == null)
            {
                throw new ArgumentNullException(nameof(cacheOptions));
            }

            _cacheEntryOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = cacheOptions.Value?.CacheDuration
            };
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter) =>
            await InvokeActionWithCacheOptionsAsync(
                key: $"torrents-{page}-{pageSize}-{filter?.GetHashCode()}",
                func: TorrentsIndexCallbackAsync(page, pageSize, filter));

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id) =>
            await InvokeActionWithCacheOptionsAsync(
                key: $"torrent-{id}",
                func: TorrentIndexCallbackAsync(id));

        public async Task<FacetViewModel<string>> GetTitleFacetAsync(int count) =>
            await InvokeActionWithCacheOptionsAsync(
                key: $"titles-{count}",
                func: TitleFacetCallbackAsync(count));

        private async Task<TResult> InvokeActionWithCacheOptionsAsync<TResult>(string key, Task<TResult> func) =>
            await _cache.GetOrCreateAsync(key, async entry =>
            {
                entry.SetOptions(_cacheEntryOptions);

                return await func;
            });
    }
}