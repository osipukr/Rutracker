using Rutracker.Core.Entities;

namespace Rutracker.Core.Interfaces
{
    public interface ITorrentRepository : IRepository<Torrent, long>
    {
    }
}