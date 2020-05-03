using System.Threading.Tasks;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.Infrastructure.Interfaces
{
    public interface ITorrentService
    {
        Task<PagedList<TorrentView>> ListAsync(TorrentFilter filter);
        Task<TorrentView> FindAsync(int id);
        Task<TorrentView> AddAsync(TorrentCreateView model);
        Task<TorrentView> UpdateAsync(int id, TorrentUpdateView model);
        Task DeleteAsync(int id);
    }
}