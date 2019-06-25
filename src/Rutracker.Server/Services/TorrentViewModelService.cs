using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Specifications;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Services
{
    public class TorrentViewModelService : ITorrentViewModelService
    {
        private readonly ITorrentRepository _torrentRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _defaultCacheDuration;

        public TorrentViewModelService(ITorrentRepository torrentRepository, IMapper mapper, IMemoryCache cache)
        {
            _torrentRepository = torrentRepository;
            _mapper = mapper;
            _cache = cache;
            _defaultCacheDuration = TimeSpan.FromMinutes(1.0);
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            if (page < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            var cacheKey = string.Format(CachedTorrentKeys.TorrentsIndex, page, pageSize, filter?.GetHashCode());

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                var filterSpecification = new TorrentsFilterSpecification(filter?.Search,
                    filter?.SelectedTitleIds,
                    filter?.SizeFrom,
                    filter?.SizeTo);

                var filterPaginatedSpecification = new TorrentsFilterPaginatedSpecification((page - 1) * pageSize,
                    pageSize,
                    filter?.Search,
                    filter?.SelectedTitleIds,
                    filter?.SizeFrom,
                    filter?.SizeTo);

                var torrents = await _torrentRepository.ListAsync(filterPaginatedSpecification);
                var torrentsResult = _mapper.Map<TorrentItemViewModel[]>(torrents);

                var totalItems = await _torrentRepository.CountAsync(filterSpecification);
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
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var cacheKey = string.Format(CachedTorrentKeys.TorrentIndex, id);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                var specification = new TorrentWithForumAndFilesSpecification(id);

                var torrent = await _torrentRepository.GetAsync(specification);
                var torrentResult = _mapper.Map<TorrentDetailsItemViewModel>(torrent);

                return new TorrentIndexViewModel
                {
                    TorrentDetailsItem = torrentResult
                };
            });
        }

        public async Task<FacetItemViewModel[]> GetTitlesAsync(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            var cacheKey = string.Format(CachedTorrentKeys.Titles, count);

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;

                var facets = await _torrentRepository.GetPopularForumsAsync(count);

                return facets.Select(x => new FacetItemViewModel
                {
                    Id = x.Id.ToString(),
                    Value = x.Value,
                    Count = x.Count.ToString(),
                    IsSelected = false
                }).ToArray();
            });
        }
    }
}