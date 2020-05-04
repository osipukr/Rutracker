using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorInputFile;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.Infrastructure.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileView>> ListAsync(int torrentId);
        Task<FileView> AddAsync(int torrentId, IFileListEntry file);
        Task<FileView> FindAsync(int id);
        Task DeleteAsync(int id);
    }
}