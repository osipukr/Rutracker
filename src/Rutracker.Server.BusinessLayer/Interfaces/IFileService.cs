using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<File>> ListAsync(int torrentId);
        Task<File> FindAsync(int id);
    }
}