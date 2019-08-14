using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, long>
    {
        Task<IReadOnlyList<(long Id, string Value, int Count)>> GetPopularForumsAsync(int count);
    }
}