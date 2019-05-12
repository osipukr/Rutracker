using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Interfaces
{
    public interface ITorrentService
    {
        Task<TorrentsViewModel> GetTorrentsAsync(int pageIndex, int itemsPage, string search, string sort, string order);
        Task<DetailsViewModel> GetTorrentAsync(long torrentId);
    }
}