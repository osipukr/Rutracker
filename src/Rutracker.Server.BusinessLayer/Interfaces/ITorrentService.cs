using System.IO;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ITorrentService
    {
        Task<IPagedList<Torrent>> ListAsync(ITorrentFilter filter);
        Task<Torrent> FindAsync(int id);
        Task<Torrent> AddAsync(Torrent torrent);
        Task<Torrent> UpdateAsync(int id, Torrent torrent);
        Task<Torrent> DeleteAsync(int id);
        Task<string> GetDownloadFileName(int id);
        Task DownloadToStreamAsync(int id, Stream outStream);
    }
}