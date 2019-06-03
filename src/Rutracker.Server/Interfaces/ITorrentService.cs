using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Interfaces
{
    public interface ITorrentService
    {
        Task<TorrentsViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter);
        Task<TorrentViewModel> GetTorrentIndexAsync(long id);
        Task<IEnumerable<FacetItem>> GetTitlesAsync(int count);
    }
}