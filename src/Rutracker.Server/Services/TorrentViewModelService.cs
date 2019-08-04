using System;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Rutracker.Core.Interfaces.Services;
using Rutracker.Server.Extensions;
using Rutracker.Server.Settings;
using Rutracker.Shared.Interfaces;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Services
{
    public class TorrentViewModelService : ITorrentViewModelService
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
            await InvokeActionAsync(
                key: $"torrents-{page}-{pageSize}-{filter?.GetHashCode()}",
                func: TorrentsIndexCallbackAsync(page, pageSize, filter));

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id) =>
            await InvokeActionAsync(
                key: $"torrent-{id}",
                func: TorrentIndexCallbackAsync(id));

        public async Task<FacetViewModel<string>> GetTitleFacetAsync(int count) =>
            await InvokeActionAsync(
                key: $"titles-{count}",
                func: TitleFacetCallbackAsync(count));

        private async Task<TResult> InvokeActionAsync<TResult>(string key, Task<TResult> func) =>
            await _cache.GetOrCreateAsync(key, () => func, _cacheEntryOptions);

        #region Cache entry callback functions

        private async Task<TorrentsIndexViewModel> TorrentsIndexCallbackAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            Guard.Against.Null(filter, nameof(filter));

            var torrentsSource = await _torrentService.GetTorrentsOnPageAsync(page, pageSize,
                filter.Search,
                filter.SelectedTitleIds,
                filter.SizeFrom,
                filter.SizeTo);

            var totalItemsCount = await _torrentService.GetTorrentsCountAsync(
                filter.Search,
                filter.SelectedTitleIds,
                filter.SizeFrom,
                filter.SizeTo);

            return new TorrentsIndexViewModel
            {
                TorrentItems = _mapper.Map<TorrentItemViewModel[]>(torrentsSource),
                PaginationModel = new PaginationViewModel
                {
                    CurrentPage = page,
                    TotalItems = totalItemsCount,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize)
                }
            };
        }

        private async Task<TorrentIndexViewModel> TorrentIndexCallbackAsync(long id)
        {
            var torrentsSource = await _torrentService.GetTorrentDetailsAsync(id);

            return new TorrentIndexViewModel
            {
                TorrentDetailsItem = _mapper.Map<TorrentDetailsItemViewModel>(torrentsSource)
            };
        }

        private async Task<FacetViewModel<string>> TitleFacetCallbackAsync(int count)
        {
            var facets = await _torrentService.GetPopularForumsAsync(count);

            return new FacetViewModel<string>
            {
                FacetItems = facets.Select(x => new FacetItemViewModel<string>
                {
                    Id = x.Id.ToString(),
                    Value = x.Value,
                    Count = x.Count
                }).ToArray()
            };
        }

        #endregion
    }
}