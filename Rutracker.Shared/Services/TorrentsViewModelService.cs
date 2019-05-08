using System;
using System.Linq;
using System.Threading.Tasks;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Specifications;
using Rutracker.Shared.Helpers;
using Rutracker.Shared.Interfaces;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Shared.Services
{
    public class TorrentsViewModelService : ITorrentsViewModelService
    {
        private readonly ITorrentRepository _torrentRepository;

        public TorrentsViewModelService(ITorrentRepository torrentRepository)
        {
            _torrentRepository = torrentRepository;
        }

        public async Task<int> GetTotalCountAsync(string search) =>
            await _torrentRepository.CountAsync(new TorrentsFilterSpecification(search));

        public async Task<TorrentsViewModel> GetTorrentsAsync(int pageIndex, int itemsPage, string search, string sort, string order)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            Enum.TryParse(sort, true, out SortPropertyStateViewModel sortProperty);
            Enum.TryParse(order, true, out SortOrderStateViewModel sortOrder);

            var sortingSpecification = new TorrentsSortPaginatedSpecification((pageIndex - 1) * itemsPage, itemsPage,
                search, sortProperty.ToString(), sortOrder.ToString());

            var torrents = await _torrentRepository.ListAsync(sortingSpecification);
            var totalItems = await GetTotalCountAsync(search);

            return new TorrentsViewModel()
            {
                TorrentItems = torrents?.Select(x => new TorrentItemViewModel(x.Id, x.Date, x.Size, x.Title)).ToArray(),
                PageModel = new PaginationInfoViewModel(totalItems, pageIndex, itemsPage),
                SortModel = new SortViewModel(sortProperty, sortOrder),
                SelectedTitle = search
            };
        }

        public async Task<TorrentDetailsViewModel> GetTorrentAsync(long torrentId)
        {
            var torrent = await _torrentRepository.GetAsync(torrentId);

            if (torrent == null)
            {
                return new TorrentDetailsViewModel();
            }

            return new TorrentDetailsViewModel()
            {
                TorrentDetailsItem = new TorrentDetailsItemViewModel(torrent.Id, torrent.Date, torrent.Size, torrent.Title)
                {
                    Content = BBCodeHelper.Parse(torrent.Content),
                    ForumTitle = torrent.ForumTitle,
                    Hash = torrent.Hash,
                    IsDeleted = torrent.IsDeleted,
                    TorrentFiles = torrent.Files?.Select(f => new FileItemViewModel(f.Size, f.Name)).ToArray()
                }
            };
        }
    }
}