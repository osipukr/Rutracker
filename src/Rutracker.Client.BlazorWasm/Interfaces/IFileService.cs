using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.BlazorWasm.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileViewModel>> ListAsync(int torrentId);
        Task<FileViewModel> CreateAsync(FileCreateViewModel model);
        Task DeleteAsync(int id);
    }
}