using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IDialogService
    {
        Task<IEnumerable<Dialog>> ListAsync(string userId);
        Task<Dialog> FindAsync(int id, string userId);
        Task<Dialog> FindByOwnerAsync(int id, string userId);
        Task<Dialog> AddAsync(Dialog dialog, IEnumerable<string> userIds);
        Task<Dialog> DeleteAsync(int id, string userId);
    }
}