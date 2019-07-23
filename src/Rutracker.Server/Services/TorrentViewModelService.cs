using System;
using System.Linq;
using System.Threading.Tasks;
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
            _torrentService = torrentService;
            _mapper = mapper;
            _cache = cache;
            _cacheEntryOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = cacheOptions.Value?.CacheDuration
            };
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var key = $"torrents-{page}-{pageSize}-{filter?.GetHashCode()}";
            var callback = TorrentsIndexCallbackAsync(page, pageSize, filter);

            return await _cache.GetOrCreateAsync(key, () => callback, _cacheEntryOptions);
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            var key = $"torrent-{id}";
            var callback = TorrentIndexCallbackAsync(id);

            return await _cache.GetOrCreateAsync(key, () => callback, _cacheEntryOptions);
        }

        public async Task<FacetViewModel<string>> GetTitleFacetAsync(int count)
        {
            var key = $"titles-{count}";
            var callback = TitleFacetCallbackAsync(count);

            return await _cache.GetOrCreateAsync(key, () => callback, _cacheEntryOptions);
        }

        #region Cache entry callback functions

        private async Task<TorrentsIndexViewModel> TorrentsIndexCallbackAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var torrentsSource = await _torrentService.GetTorrentsOnPageAsync(page, pageSize,
                filter?.Search,
                filter?.SelectedTitleIds,
                filter?.SizeFrom,
                filter?.SizeTo);

            var totalItemsCount = await _torrentService.GetTorrentsCountAsync(
                filter?.Search,
                filter?.SelectedTitleIds,
                filter?.SizeFrom,
                filter?.SizeTo);

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