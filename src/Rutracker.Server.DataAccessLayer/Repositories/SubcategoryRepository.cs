using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class SubcategoryRepository : Repository<Subcategory, int>, ISubcategoryRepository
    {
        public SubcategoryRepository(RutrackerContext context) : base(context)
        {
        }

        public IQueryable<Subcategory> GetAll(int categoryId)
        {
            return GetAll(x => x.CategoryId == categoryId);
        }

        public async Task<Subcategory> GetAsync(int subcategoryId, int categoryId)
        {
            return await GetAsync(x => x.Id == subcategoryId && x.CategoryId == categoryId);
        }
    }
}