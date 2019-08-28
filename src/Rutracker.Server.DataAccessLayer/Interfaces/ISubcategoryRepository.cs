using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface ISubcategoryRepository : IRepository<Subcategory, int>
    {
        IQueryable<Subcategory> GetAll(int categoryId);
        Task<Subcategory> GetAsync(int subcategoryId, int categoryId);
    }
}