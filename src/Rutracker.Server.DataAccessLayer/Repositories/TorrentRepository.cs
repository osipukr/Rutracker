using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class TorrentRepository : Repository<Torrent, int>, ITorrentRepository
    {
        public TorrentRepository(RutrackerContext context) : base(context)
        {
        }
    }
}