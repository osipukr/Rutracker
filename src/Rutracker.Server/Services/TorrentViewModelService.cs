using System;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Rutracker.Core.Interfaces;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Settings;
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
                SlidingExpiration = cacheOptions.Value.DefaultCacheDuration
            };
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            Guard.Against.OutOfRange(page, nameof(page), 1, int.MaxValue);
            Guard.Against.OutOfRange(pageSize, nameof(pageSize), 1, int.MaxValue);

            var cacheKey = $"torrents-{page}-{pageSize}-{filter?.GetHashCode()}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetOptions(_cacheEntryOptions);

                return await TorrentsIndexCallbackAsync(page, pageSize, filter);
            });
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            Guard.Against.OutOfRange((int)id, nameof(id), 1, int.MaxValue);

            var cacheKey = $"torrent-{id}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetOptions(_cacheEntryOptions);

                return await TorrentIndexCallbackAsync(id);
            });
        }

        public async Task<FacetViewModel<string>> GetTitleFacetAsync(int count)
        {
            Guard.Against.OutOfRange(count, nameof(count), 1, int.MaxValue);

            var cacheKey = $"titles-{count}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetOptions(_cacheEntryOptions);

                return await TitleFacetCallbackAsync(count);
            });
        }

        #region Cache entry callback functions

        private async Task<TorrentsIndexViewModel> TorrentsIndexCallbackAsync(int page,
            int pageSize,
            FiltrationViewModel filter)
        {
            var torrents = await _torrentService.GetTorrentsOnPageAsync(page, pageSize, filter?.Search,
                filter?.SelectedTitleIds, filter?.SizeFrom, filter?.SizeTo);

            var totalItems = await _torrentService.GetTorrentsCountAsync(filter?.Search, filter?.SelectedTitleIds,
                filter?.SizeFrom, filter?.SizeTo);

            var torrentsResult = _mapper.Map<TorrentItemViewModel[]>(torrents);
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return new TorrentsIndexViewModel
            {
                TorrentItems = torrentsResult,
                PaginationModel = new PaginationViewModel
                {
                    CurrentPage = page,
                    TotalItems = totalItems,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    HasPrevious = page > 1 && totalPages > 1,
                    HasNext = page < totalPages
                }
            };
        }

        private async Task<TorrentIndexViewModel> TorrentIndexCallbackAsync(long id)
        {
            var torrent = await _torrentService.GetTorrentDetailsAsync(id);
            var torrentResult = _mapper.Map<TorrentDetailsItemViewModel>(torrent);

            return new TorrentIndexViewModel
            {
                TorrentDetailsItem = torrentResult
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