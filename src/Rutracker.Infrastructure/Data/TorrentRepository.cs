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

        public async Task<IReadOnlyList<long>> GetPopularForumsAsync(int count)
        {
            return await _context.Torrents.AsNoTracking()
                .GroupBy(x => x.ForumId)
                .OrderByDescending(x => x.Count())
                .Take(count)
                .Select(x => x.Key)
                .ToListAsync();

        }

        public async Task<int> GetForumsCountAsync(long forumId) =>
            await _context.Torrents.CountAsync(x => x.ForumId == forumId);
    }
}