using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface ITorrentService
    {
        Task<PaginationResult<TorrentViewModel>> ListAsync(int page, int pageSize, FilterViewModel filter);
        Task<IEnumerable<TorrentViewModel>> PopularTorrentsAsync(int count);
        Task<TorrentDetailsViewModel> FindAsync(int id);
    }
}