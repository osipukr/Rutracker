using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.Torrents;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface ITorrentViewModelService
    {
        Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter);
        Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id);
        Task<FacetViewModel<string>> GetTitleFacetAsync(int count);
    }
}