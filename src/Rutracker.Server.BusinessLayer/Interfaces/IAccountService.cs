using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        Task<User> CreateUserAsync(string userName, string email, string password);
        Task<User> CheckUserAsync(string userName, string password);
        Task<IReadOnlyList<string>> GetUserRolesAsync(User user);
        Task LogOutUserAsync();
    }
}