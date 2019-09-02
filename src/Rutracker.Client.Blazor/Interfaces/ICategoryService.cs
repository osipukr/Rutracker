using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.Torrent.Create;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> ListAsync();
        Task<CategoryViewModel> FindAsync(int id);
        Task<CategoryViewModel> CreateAsync(CategoryCreateViewModel model);
        Task DeleteAsync(int id);
    }
}