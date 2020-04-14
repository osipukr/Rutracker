using System.Threading.Tasks;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.BusinessLayer.Interfaces
{
    public interface ITorrentService
    {
        Task<IPagedList<TorrentView>> ListAsync(ITorrentFilter filter);
        Task<TorrentDetailView> FindAsync(int id);
        Task<TorrentDetailView> AddAsync(TorrentCreateView model);
        Task<TorrentDetailView> UpdateAsync(int id, TorrentUpdateView model);
        Task DeleteAsync(int id);
    }
}