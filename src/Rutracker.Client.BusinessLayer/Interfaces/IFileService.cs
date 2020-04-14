using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Client.BusinessLayer.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileView>> ListAsync(int torrentId);
        Task<FileView> FindAsync(int id);
    }
}