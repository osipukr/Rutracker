using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<File>> ListAsync(int torrentId);
        Task<File> FindAsync(int id);
        Task<File> FindAsync(int id, string userId);
        Task<File> AddAsync(string userId, File file);
        Task<File> UpdateAsync(int id, string userId, File file);
        Task<File> DeleteAsync(int id, string userId);
    }
}