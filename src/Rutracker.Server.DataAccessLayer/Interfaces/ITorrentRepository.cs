using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, int>
    {
    }
}