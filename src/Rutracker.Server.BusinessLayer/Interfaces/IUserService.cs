using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IPagedList<User>> ListAsync(IUserFilter filter);
        Task<User> AddAsync(User user);
        Task<User> AddAsync(User user, string password);
        Task<User> AddToRoleAsync(string userId, string roleName);
        Task<User> FindAsync(string userId);
        Task<User> FindByNameAsync(string userName);
        Task<bool> ExistAsync(string userName);
        Task<User> UpdateAsync(string userId, User user);
        Task<IEnumerable<Role>> RolesAsync(string userId);
        Task<string> PasswordResetTokenAsync(string userId);
        Task<User> ResetPasswordAsync(string userId, string password, string token);
        Task<User> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}