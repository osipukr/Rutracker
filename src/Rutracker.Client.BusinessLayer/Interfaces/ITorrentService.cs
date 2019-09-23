using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.BusinessLayer.Interfaces
{
    public interface ITorrentService
    {
        Task<PaginationResult<TorrentViewModel>> ListAsync(int page, int pageSize, TorrentFilterViewModel filter);
        Task<IEnumerable<TorrentViewModel>> PopularAsync(int count);
        Task<TorrentDetailsViewModel> FindAsync(int id);
        Task<TorrentDetailsViewModel> CreateAsync(TorrentCreateViewModel model);
        Task<TorrentDetailsViewModel> UpdateAsync(int id, TorrentUpdateViewModel model);
        Task DeleteAsync(int id);
        Task<string> ChangeImageAsync(int id, ChangeTorrentImageViewModel model);
        Task<string> ChangeImageAsync(int id, IFileReference imageReference);
        Task DeleteImageAsync(int id);
    }
}