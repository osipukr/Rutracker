using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.FileReader;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.BlazorWasm.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileViewModel>> ListAsync(int torrentId);
        Task<FileViewModel> AddAsync(int torrentId, IFileReference file);
        Task DeleteAsync(int id);
        Task<string> DownloadAsync(int id);
    }
}