using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, long>
    {
        Task<IEnumerable<string>> GetPopularForumsAsync(int count);
    }
}