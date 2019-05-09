using Rutracker.Server.Core.Entities;

namespace Rutracker.Server.Core.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, long>
    {
    }
}