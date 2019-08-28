using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ITorrentService
    {
        Task<IEnumerable<Torrent>> ListAsync(int page, int pageSize, string search, long? sizeFrom, long? sizeTo);
        Task<Torrent> FindAsync(int id);
        Task<int> CountAsync(string search, long? sizeFrom, long? sizeTo);
    }
}