using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface ITorrentService
    {
        Task<PaginationResult<TorrentViewModel>> ListAsync(int page, int pageSize, TorrentFilterViewModel filter);
        Task<IEnumerable<TorrentViewModel>> PopularAsync(int count);
        Task<TorrentDetailsViewModel> FindAsync(int id);
        Task<TorrentDetailsViewModel> CreateAsync(TorrentCreateViewModel model);
        Task<TorrentDetailsViewModel> Update(int id, TorrentUpdateViewModel model);
        Task DeleteAsync(int id);
    }
}