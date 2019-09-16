using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using File = Rutracker.Server.DataAccessLayer.Entities.File;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<File>> ListAsync(int torrentId);
        Task<File> FindAsync(int id);
        Task<File> FindAsync(int id, string userId);
        Task<File> AddAsync(string userId, int torrentId, string mimeType, string fileName, Stream fileStream);
        Task<File> DeleteAsync(int id, string userId);
    }
}