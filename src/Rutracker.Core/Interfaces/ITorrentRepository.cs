using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, long>
    {
        Task<IReadOnlyList<long>> GetPopularForumsAsync(int count);
        Task<int> GetForumsCountAsync(long forumId);
    }
}