using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Client.BlazorWasm.Interfaces
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<SubcategoryViewModel>> ListAsync(int categoryId);
        Task<SubcategoryViewModel> FindAsync(int id);
        Task<SubcategoryViewModel> CreateAsync(SubcategoryCreateViewModel model);
        Task<SubcategoryViewModel> UpdateAsync(int id, SubcategoryUpdateViewModel model);
        Task DeleteAsync(int id);
    }
}