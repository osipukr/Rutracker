using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.Infrastructure.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileView>> ListAsync(int torrentId);
        Task<FileView> FindAsync(int id);
    }
}