using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Interfaces
{
    public interface ITorrentsClientService
    {
        Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter);
        Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id);
        Task<FacetViewModel> GetTitlesAsync(int count);
    }
}