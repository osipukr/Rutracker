using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IReadOnlyList<string>> GetPopularForumsAsync(int count) =>
            await _context.Torrents.GroupBy(x => x.Forum.Title).OrderByDescending(x => x.Count()).Take(count)
                .Select(x => x.Key).ToListAsync();
    }
}