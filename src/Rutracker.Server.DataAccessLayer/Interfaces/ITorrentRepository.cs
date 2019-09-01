using System.Linq;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, int>
    {
        IQueryable<Torrent> Search(int? categoryId, int? subcategoryId, string search);
        IQueryable<Torrent> PopularTorrents(int count);
    }
}