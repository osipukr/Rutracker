using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface ITorrentService
    {
        Task<PaginationResult<TorrentViewModel>> Torrents(int page, int pageSize, FilterViewModel filter);
        Task<IEnumerable<TorrentViewModel>> Popular(int count);
        Task<TorrentDetailsViewModel> Torrent(int id);
    }
}