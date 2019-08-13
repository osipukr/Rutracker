using System;
using System.Linq;
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

        public IQueryable<Torrent> Search(string search, long[] forumIds, long? sizeFrom, long? sizeTo) =>
            GetAll(torrent =>
                (string.IsNullOrWhiteSpace(search) || torrent.Title.Contains(search)) &&
                (forumIds == null || forumIds.Length == 0 || forumIds.Contains(torrent.ForumId)) &&
                (!sizeFrom.HasValue || torrent.Size >= sizeFrom) &&
                (!sizeTo.HasValue || torrent.Size <= sizeTo));

        public IQueryable<(long Id, string Value, int Count)> GetForums(int count) =>
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
                    (g, f) => ValueTuple.Create(g.Key, f.Title, g.Count));
    }
}