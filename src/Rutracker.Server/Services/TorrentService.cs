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

        public TorrentService(ITorrentRepository torrentRepository)
        {
            _torrentRepository = torrentRepository;
        }

        public async Task<TorrentsViewModel> GetTorrentsIndexAsync(int page, int pageSize, string search)
        {
            var filterSpecification = new TorrentsFilterSpecification(search);
            var filterPaginatedSpecification = new TorrentsFilterPaginatedSpecification((page - 1) * pageSize, pageSize, search);

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
                    Content = BBCodeHelper.Parse(torrent.Content),
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
            var titles = await _torrentRepository.GetPopularForumsAsync(count);

            return titles.Select(x => new FacetItem
            {
                Value = x
            });
        }
    }
}