using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<Category> FindAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(int id, Category category);
        Task<Category> DeleteAsync(int id);
    }
}