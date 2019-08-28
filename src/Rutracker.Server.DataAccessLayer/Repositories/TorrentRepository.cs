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
                .Include(x => x.Files)
                .Include(x => x.Comments)
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
    }
}