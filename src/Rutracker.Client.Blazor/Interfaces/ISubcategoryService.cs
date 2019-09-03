using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<SubcategoryViewModel>> ListAsync(int categoryId);
        Task<SubcategoryViewModel> FindAsync(int id);
    }
}