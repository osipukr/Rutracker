using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Interfaces
{
    public interface ITorrentService
    {
        Task<int> GetTotalCountAsync(string search);
        Task<TorrentsViewModel> GetTorrentsAsync(int pageIndex, int itemsPage, string search, string sort,
            string order);
        Task<TorrentDetailsViewModel> GetTorrentAsync(long torrentId);
    }
}