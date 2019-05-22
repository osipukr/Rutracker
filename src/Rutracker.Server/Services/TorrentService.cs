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
                PaginationModel = new PaginationViewModel(totalItems, pageIndex, itemsPage)
            };
        }

        public async Task<DetailsViewModel> GetTorrentAsync(long torrentId)
        {
            var torrent = await _torrentRepository.GetAsync(torrentId);

            return new DetailsViewModel()
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

        public async Task<FiltrationViewModel> GetFiltrationAsync(int count)
        {
            var titles = await _torrentRepository.GetPopularForumsAsync(count);

            return new FiltrationViewModel()
            {
                PopularForums = titles
            };
        }
    }
}