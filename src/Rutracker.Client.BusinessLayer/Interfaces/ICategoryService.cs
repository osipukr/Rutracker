using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Category;

namespace Rutracker.Client.BusinessLayer.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryView>> ListAsync();
        Task<CategoryDetailView> FindAsync(int id);
        Task<CategoryView> AddAsync(CategoryCreateView model);
        Task<CategoryView> UpdateAsync(int id, CategoryUpdateView model);
        Task DeleteAsync(int id);
    }
}