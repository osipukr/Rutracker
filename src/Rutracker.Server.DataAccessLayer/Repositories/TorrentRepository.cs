using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class TorrentRepository : Repository<Torrent, long>, ITorrentRepository
    {
        public TorrentRepository(TorrentContext context)
            : base(context)
        {
        }

        public override async Task<Torrent> GetAsync(long id) =>
            await _dbSet.Include(x => x.Forum)
                .Include(x => x.Files)
                .SingleOrDefaultAsync(x => x.Id == id);

        public IQueryable<Torrent> Search(string search, long[] forumIds, long? sizeFrom, long? sizeTo) =>
            GetAll(torrent =>
                (string.IsNullOrWhiteSpace(search) || torrent.Title.Contains(search)) &&
                (forumIds == null || forumIds.Length == 0 || forumIds.Contains(torrent.ForumId)) &&
                (!sizeFrom.HasValue || torrent.Size >= sizeFrom) &&
                (!sizeTo.HasValue || torrent.Size <= sizeTo));

        public IQueryable<Tuple<long, string, int>> GetForums(int count) =>
            _dbSet.GroupBy(x => x.ForumId,
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
                    (g, f) => Tuple.Create(g.Key, f.Title, g.Count));
    }
}