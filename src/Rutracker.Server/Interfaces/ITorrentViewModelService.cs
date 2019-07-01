using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Interfaces
{
    public interface ITorrentViewModelService
    {
        Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter);
        Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id);
        Task<FacetViewModel> GetTitleFacetAsync(int count);
    }
}