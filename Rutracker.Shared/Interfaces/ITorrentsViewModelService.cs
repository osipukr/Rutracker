using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Shared.Interfaces
{
    public interface ITorrentsViewModelService
    {
        Task<int> GetTotalCountAsync(string search);
        Task<TorrentsViewModel> GetTorrentsAsync(int pageIndex, int itemsPage, string search, string sort,
            string order);
        Task<TorrentDetailsViewModel> GetTorrentAsync(long torrentId);
    }
}