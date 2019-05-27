using System.Threading.Tasks;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Interfaces
{
    public interface ITorrentService
    {
        Task<TorrentsViewModel> GetTorrentsAsync(int pageIndex, int itemsPage, string search, FiltrationViewModel filter);
        Task<DetailsViewModel> GetTorrentAsync(long torrentId);
        Task<FiltrationViewModel> GetTorrentFilterAsync(int forumCount);
    }
}