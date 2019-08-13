using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface ITorrentViewModelService
    {
        Task<PaginationResult<TorrentViewModel>> TorrentsAsync(int page, int pageSize, FilterViewModel filter);
        Task<TorrentDetailsViewModel> TorrentAsync(long id);
        Task<FacetResult<string>> ForumFacetAsync(int count);
    }
}