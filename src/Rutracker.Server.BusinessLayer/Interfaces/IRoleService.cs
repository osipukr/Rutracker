using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IRoleService
    {
        Task<Role> FindAsync(string roleId);
        Task<Role> FindByNameAsync(string roleName);
        Task<Role> AddAsync(Role role);
        Task<bool> ExistAsync(string roleName);
    }
}