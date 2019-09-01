using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Client.Blazor.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> ListAsync();
        Task<CategoryViewModel> FindAsync(int id);
    }
}