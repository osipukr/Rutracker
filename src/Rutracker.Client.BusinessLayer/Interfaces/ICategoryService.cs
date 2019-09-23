using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Category;

namespace Rutracker.Client.BusinessLayer.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> ListAsync();
        Task<CategoryViewModel> FindAsync(int id);
        Task<CategoryViewModel> CreateAsync(CategoryCreateViewModel model);
        Task<CategoryViewModel> UpdateAsync(int id, CategoryUpdateViewModel model);
        Task DeleteAsync(int id);
    }
}