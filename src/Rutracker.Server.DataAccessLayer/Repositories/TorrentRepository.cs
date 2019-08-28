using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public override async Task<Torrent> GetAsync(int id)
        {
            return await GetAll()
                .Include(x => x.Subcategory)
                .Include(x => x.User)
                .Include(x => x.Files)
                .Include(x => x.Comments).ThenInclude(x => x.User)
                .Include(x => x.Comments).ThenInclude(x => x.Likes)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Torrent> GetAll(string userId)
        {
            return GetAll(x => x.UserId == userId);
        }

        public IQueryable<Torrent> Search(string search, long? sizeFrom, long? sizeTo)
        {
            return GetAll(torrent =>
                (string.IsNullOrWhiteSpace(search) || torrent.Name.Contains(search)) &&
                (!sizeFrom.HasValue || torrent.Size >= sizeFrom) &&
                (!sizeTo.HasValue || torrent.Size <= sizeTo))
                .Include(x => x.Comments);
        }

        public IQueryable<Torrent> PopularTorrents(int count)
        {
            return _context.Comments.GroupBy(x => x.TorrentId,
                    (key, items) => new
                    {
                        Key = key,
                        Count = items.Count()
                    })
                .OrderByDescending(x => x.Count)
                .Take(count)
                .Join(_dbSet, g => g.Key, t => t.Id, (g, t) => t);
        }
    }
}