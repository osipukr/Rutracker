using System.Threading.Tasks;
using Rutracker.Shared.ViewModels.Shared;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Interfaces.Controllers
{
    public interface ITorrentsController
    {
        Task<TorrentsIndexViewModel> Pagination(int page, int pageSize, FiltrationViewModel filter);
        Task<TorrentIndexViewModel> Get(long id);
        Task<FacetViewModel<string>> Titles(int count);
    }
}