using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Rutracker.Core.Interfaces;
using Rutracker.Server.Interfaces;
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
        private readonly TimeSpan _defaultCacheDuration;

        public TorrentViewModelService(ITorrentService torrentService, IMapper mapper, IMemoryCache cache)
        {
            _torrentService = torrentService;
            _mapper = mapper;
            _cache = cache;
            _defaultCacheDuration = TimeSpan.FromMinutes(1.0);
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var cacheKey = string.Format(CachedTorrentKeys.TorrentsIndex, page, pageSize, filter?.GetHashCode());

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

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
                        HasPrevious = page > 1 && page <= totalPages,
                        HasNext = page < totalPages
                    }
                };
            });
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            var cacheKey = string.Format(CachedTorrentKeys.TorrentIndex, id);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                var torrent = await _torrentService.GetTorrentDetailsAsync(id);
                var torrentResult = _mapper.Map<TorrentDetailsItemViewModel>(torrent);

                return new TorrentIndexViewModel
                {
                    TorrentDetailsItem = torrentResult
                };
            });
        }

        public async Task<FacetViewModel> GetTitleFacetAsync(int count)
        {
            var cacheKey = string.Format(CachedTorrentKeys.Titles, count);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                var facets = await _torrentService.GetPopularForumsAsync(count);

                return new FacetViewModel
                {
                    FacetItems = facets.Select(x => new FacetItemViewModel
                    {
                        Id = x.Id.ToString(),
                        Value = x.Value,
                        Count = x.Count.ToString(),
                        IsSelected = false
                    }).ToArray()
                };
            });
        }
    }
}