using Rutracker.Server.Core.Entities;
using Rutracker.Server.Core.Interfaces;

namespace Rutracker.Server.Infrastructure.Data
{
    public class TorrentRepository : Repository<Torrent, long>, ITorrentRepository
    {
        public TorrentRepository(TorrentContext context) 
            : base(context)
        {
        }
    }
}