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
    }
}