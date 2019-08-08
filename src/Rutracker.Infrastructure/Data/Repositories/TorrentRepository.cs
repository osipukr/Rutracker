using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities.Torrents;
using Rutracker.Core.Interfaces.Repositories;
using Rutracker.Infrastructure.Data.Contexts;

namespace Rutracker.Infrastructure.Data.Repositories
{
    public class TorrentRepository : Repository<Torrent, long>, ITorrentRepository
    {
        public TorrentRepository(TorrentContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<(long Id, string Value, int Count)>> GetPopularForumsAsync(int count) =>
            await _dbSet.GroupBy(x => x.ForumId,
                    (key, items) => new
                    {
                        Key = key,
                        Count = items.Count()
                    })
                .OrderByDescending(x => x.Count)
                .Take(count)
                .Join(_context.Forums,
                    g => g.Key,
                    f => f.Id,
                    (g, f) => ValueTuple.Create(g.Key, f.Title, g.Count))
                .ToListAsync();
    }
}