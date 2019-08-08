using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services.Interfaces
{
    public interface ITorrentService
    {
        Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter);
        Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id);
        Task<FacetViewModel<string>> GetTitleFacetAsync(int count);
    }
}