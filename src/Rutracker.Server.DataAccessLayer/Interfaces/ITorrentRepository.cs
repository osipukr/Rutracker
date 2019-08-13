using System.Linq;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, long>
    {
        IQueryable<Torrent> Search(string search, long[] forumIds, long? sizeFrom, long? sizeTo);
        IQueryable<(long Id, string Value, int Count)> GetForums(int count);
    }
}