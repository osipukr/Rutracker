using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<Subcategory>> ListAsync(int categoryId);
        Task<Subcategory> FindAsync(int subcategoryId);
    }
}