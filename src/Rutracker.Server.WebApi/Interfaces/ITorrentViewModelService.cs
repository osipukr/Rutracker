using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Shared;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Interfaces
{
    public interface ITorrentViewModelService
    {
        Task<PaginationResult<TorrentViewModel>> GetTorrentsIndexAsync(int page, int pageSize, FilterViewModel filter);
        Task<TorrentDetailsViewModel> GetTorrentIndexAsync(long id);
        Task<FacetResult<string>> GetTitleFacetAsync(int count);
    }
}