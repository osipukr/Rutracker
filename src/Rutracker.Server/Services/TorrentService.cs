using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Specifications;
using Rutracker.Server.Helpers;
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

        public TorrentService(ITorrentRepository torrentRepository, IForumRepository forumRepository)
        {
            _torrentRepository = torrentRepository;
            _forumRepository = forumRepository;
        }

        public async Task<TorrentsViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter)
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
            var totalItems = await _torrentRepository.CountAsync(filterSpecification);

            return new TorrentsViewModel
            {
                TorrentItems = torrents?.Select(x => new TorrentItemViewModel
                {
                    Id = x.Id,
                    Size = x.Size,
                    Date = x.Date,
                    Title = x.Title
                }),
                PaginationModel = new PaginationViewModel
                {
                    CurrentPage = page,
                    TotalItems = totalItems,
                    PageSize = pageSize
                }
            };
        }

        public async Task<TorrentViewModel> GetTorrentIndexAsync(long torrentId)
        {
            var torrent = await _torrentRepository.GetAsync(torrentId);

            return new TorrentViewModel
            {
                TorrentDetailsItem = torrent == null ? null : new TorrentDetailsItemViewModel
                {
                    Id = torrent.Id,
                    Size = torrent.Size,
                    Date = torrent.Date,
                    Title = torrent.Title,
                    Hash = torrent.Hash,
                    ForumTitle = torrent.Forum.Title,
                    IsDeleted = torrent.IsDeleted,
                    Content = BBCodeHelper.ParseToHtml(torrent.Content),
                    Files = torrent.Files?.Select(x => new FileItemViewModel
                    {
                        Size = x.Size,
                        Name = x.Name
                    })
                }
            };
        }

        public async Task<IEnumerable<FacetItem>> GetTitlesAsync(int count)
        {
            var forumsId = await _torrentRepository.GetPopularForumsAsync(count);

            var tasks = forumsId?.Select(async id =>
            {
                var forum = await _forumRepository.GetAsync(id);
                var forumCount = await _torrentRepository.GetForumsCountAsync(id);

                return new FacetItem
                {
                    Id = id.ToString(),
                    Value = forum.Title,
                    Count = forumCount
                };
            });

            return await Task.WhenAll(tasks);
        }
    }
}