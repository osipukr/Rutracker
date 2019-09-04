using System.Linq;
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

        public IQueryable<Torrent> Search(int? categoryId, int? subcategoryId, string search)
        {
            return GetAll(torrent =>
                (!subcategoryId.HasValue || torrent.SubcategoryId == subcategoryId) &&
                (!categoryId.HasValue || torrent.Subcategory.CategoryId == categoryId) &&
                (string.IsNullOrWhiteSpace(search) || torrent.Name.Contains(search)));
        }
    }
}