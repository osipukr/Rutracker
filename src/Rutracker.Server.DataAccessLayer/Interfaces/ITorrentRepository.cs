using System.Linq;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, int>
    {
        IQueryable<Torrent> Search(string search, long? sizeFrom, long? sizeTo);
        IQueryable<Torrent> PopularTorrents(int count);
    }
}