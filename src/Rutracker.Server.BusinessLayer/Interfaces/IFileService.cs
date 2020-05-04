using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<File>> ListAsync(int torrentId);
        Task<File> AddAsync(int torrentId, IFormFile file);
        Task<File> FindAsync(int id);
        Task<File> Delete(int id);
    }
}