using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Client.Services.Interfaces
{
    public interface ITorrentService
    {
        Task<TorrentsIndexViewModel> Torrents(int page, int pageSize, FiltrationViewModel filter);
        Task<TorrentIndexViewModel> Torrent(long id);
        Task<FacetViewModel<string>> TitleFacet(int count);
    }
}