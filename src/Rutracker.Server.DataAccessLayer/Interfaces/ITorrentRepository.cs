using System;
using System.Linq;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, int>
    {
        IQueryable<Torrent> Search(string search, long[] forumIds, long? sizeFrom, long? sizeTo);
        IQueryable<Tuple<long, string, int>> GetForums(int count);
    }
}