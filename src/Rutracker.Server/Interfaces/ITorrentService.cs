using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.ViewModels;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Interfaces
{
    public interface ITorrentService
    {
        Task<TorrentsIndexViewModel> GetTorrentsIndexAsync(int page, int pageSize, FiltrationViewModel filter);
        Task<TorrentIndexViewModel> GetTorrentIndexAsync(long id);
        Task<IEnumerable<FacetItem>> GetTitlesAsync(int count);
    }
}