using Rutracker.Core.Entities;
using Rutracker.Core.Interfaces;

namespace Rutracker.Infrastructure.Data
{
    public class TorrentRepository : Repository<Torrent, long>, ITorrentRepository
    {
        public TorrentRepository(TorrentContext context) 
            : base(context)
        {
        }
    }
}