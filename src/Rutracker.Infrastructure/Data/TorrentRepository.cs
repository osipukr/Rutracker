using System;
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

        public async Task<IReadOnlyList<(long Id, string Value, int Count)>> GetPopularForumsAsync(int count)
        {
            return await _context.Torrents.GroupBy(
                    x => x.ForumId,
                    x => x.Forum.Title,
                    (id, titles) => ValueTuple.Create(id, titles.FirstOrDefault(), titles.Count()))
                .OrderByDescending(x => x.Item3)
                .Take(count)
                .ToListAsync();
        }
    }
}