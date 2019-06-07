using System;
using System.Collections.Generic;
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
    public class TorrentService : ITorrentService
    {
        private readonly ITorrentRepository _torrentRepository;
        private readonly IForumRepository _forumRepository;
        private readonly IMapper _mapper;

        public TorrentService(ITorrentRepository torrentRepository,
                              IForumRepository forumRepository,
                              IMapper mapper)
        {
            _torrentRepository = torrentRepository;
            _forumRepository = forumRepository;
            _mapper = mapper;
        }

        public async Task<TorrentsViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
        {
            if(filter == null)
            {
                filter = new FiltrationViewModel();
            }

            var filterSpecification = new TorrentsFilterSpecification(filter.Search,
                                                                      filter.SelectedTitles,
                                                                      filter.SizeFrom,
                                                                      filter.SizeTo);

            var filterPaginatedSpecification = new TorrentsFilterPaginatedSpecification((page - 1) * pageSize,
                                                                                        pageSize,
                                                                                        filter.Search,
                                                                                        filter.SelectedTitles,
                                                                                        filter.SizeFrom,
                                                                                        filter.SizeTo);

            var torrents = await _torrentRepository.ListAsync(filterPaginatedSpecification);
            var totalItems = await _torrentRepository.CountAsync(filterSpecification);
            var torrentsResult = _mapper.Map<IEnumerable<TorrentItemViewModel>>(torrents);
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return new TorrentsViewModel
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

        public async Task<TorrentViewModel> GetTorrentIndexAsync(long id)
        {
            var torrent = await _torrentRepository.GetAsync(id);
            var torrentResult = _mapper.Map<TorrentDetailsItemViewModel>(torrent);

            return new TorrentViewModel
            {
                TorrentDetailsItem = torrentResult
            };
        }

        public async Task<IEnumerable<FacetItem>> GetTitlesAsync(int count)
        {
            var forumsId = await _torrentRepository.GetPopularForumsAsync(count);

            return await Task.WhenAll(forumsId?.Select(async id =>
            {
                var forum = await _forumRepository.GetAsync(id);
                var forumCount = await _torrentRepository.GetForumsCountAsync(id);

                return new FacetItem
                {
                    Id = id.ToString(),
                    Value = forum.Title,
                    Count = forumCount
                };
            }));
        }
    }
}