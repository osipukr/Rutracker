using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, long>
    {
        Task<IReadOnlyList<(long Id, string Value, int Count)>> GetPopularForumsAsync(int count);
    }
}