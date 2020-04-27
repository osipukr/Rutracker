using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Client.Infrastructure.Interfaces
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<SubcategoryView>> ListAsync(int? categoryId);
        Task<SubcategoryView> FindAsync(int id);
        Task<SubcategoryView> AddAsync(SubcategoryCreateView model);
        Task<SubcategoryView> UpdateAsync(int id, SubcategoryUpdateView model);
        Task DeleteAsync(int id);
    }
}