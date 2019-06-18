using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public TorrentViewModelService(ITorrentRepository torrentRepository, IMapper mapper)
        {
            _torrentRepository = torrentRepository;
            _mapper = mapper;
        }

        public async Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            var filterSpecification = new TorrentsFilterSpecification(filter?.Search,
                                                                      filter?.SelectedTitles,
                                                                      filter?.SizeFrom,
                                                                      filter?.SizeTo);

            var filterPaginatedSpecification = new TorrentsFilterPaginatedSpecification((page - 1) * pageSize,
                                                                                        pageSize,
                                                                                        filter?.Search,
                                                                                        filter?.SelectedTitles,
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
        }

        public async Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id)
        {
            var specification = new TorrentWithForumAndFilesSpecification(id);

            var torrent = await _torrentRepository.GetAsync(specification);
            var torrentResult = _mapper.Map<TorrentDetailsItemViewModel>(torrent);

            return new TorrentIndexViewModel
            {
                TorrentDetailsItem = torrentResult
            };
        }

        public async Task<FacetItemViewModel[]> GetTitlesAsync(int count)
        {
            var facets = await _torrentRepository.GetPopularForumsAsync(count);

            return facets.Select(x => new FacetItemViewModel
            {
                Id = x.Id.ToString(),
                Value = x.Value,
                Count = x.Count.ToString(),
                IsSelected = false
            }).ToArray();
        }
    }
}