using System.Linq;
using System.Threading.Tasks;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Specifications;
using Rutracker.Server.Helpers;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels;
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

        public async Task<TorrentsViewModel> GetTorrentsAsync(int pageIndex, int itemsPage, string search)
        {
            var torrents = await _torrentRepository.ListAsync(new TorrentsFilterPaginatedSpecification((pageIndex - 1) * itemsPage, itemsPage, search));
            var totalItems = await _torrentRepository.CountAsync(new TorrentsFilterSpecification(search));

            return new TorrentsViewModel()
            {
                TorrentItems = torrents?.Select(x => new TorrentItemViewModel
                {
                    Id = x.Id,
                    Size = x.Size,
                    Date = x.Date,
                    Title = x.Title
                }),
                PageModel = new PaginationInfoViewModel(totalItems, pageIndex, itemsPage),
                SelectedTitle = search
            };
        }

        public async Task<DetailsViewModel> GetTorrentAsync(long torrentId)
        {
            var torrent = await _torrentRepository.GetAsync(torrentId);

            if (torrent == null)
            {
                return new DetailsViewModel();
            }

            return new DetailsViewModel()
            {
                TorrentDetailsItem = new TorrentDetailsItemViewModel
                {
                    Id = torrent.Id,
                    Size = torrent.Size,
                    Date = torrent.Date,
                    Title = torrent.Title,
                    Hash = torrent.Hash,
                    ForumTitle = torrent.ForumTitle,
                    IsDeleted = torrent.IsDeleted,
                    Content = BBCodeHelper.Parse(torrent.Content),
                    Files = torrent.Files.Select(x => new FileItemViewModel
                    {
                        Size = x.Size,
                        Name = x.Name
                    })
                }
            };
        }
    }
}