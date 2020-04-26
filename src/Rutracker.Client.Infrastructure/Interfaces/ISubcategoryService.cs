using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Client.Infrastructure.Interfaces
{
    public interface ISubcategoryService
    {
        Task<List<SubcategoryView>> ListAsync(int? categoryId);
        Task<SubcategoryDetailView> FindAsync(int id);
        Task<SubcategoryView> AddAsync(SubcategoryCreateView model);
        Task<SubcategoryView> UpdateAsync(int id, SubcategoryUpdateView model);
        Task DeleteAsync(int id);
    }
}