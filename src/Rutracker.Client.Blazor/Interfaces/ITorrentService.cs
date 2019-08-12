using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.Torrents;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface ITorrentService
    {
        Task<TorrentsIndexViewModel> Torrents(int page, int pageSize, FiltrationViewModel filter);
        Task<TorrentIndexViewModel> Torrent(long id);
        Task<FacetViewModel<string>> TitleFacet(int count);
    }
}