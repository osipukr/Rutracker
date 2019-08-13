using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface ITorrentService
    {
        Task<PaginationResult<TorrentViewModel>> Torrents(int page, int pageSize, FiltrationViewModel filter);
        Task<TorrentDetailsViewModel> Torrent(long id);
        Task<FacetResult<string>> TitleFacet(int count);
    }
}