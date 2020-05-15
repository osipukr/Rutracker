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
        Task<User> AddToRoleAsync(User user, string roleName);
        Task<User> AddToRoleAsync(string userId, string roleName);
        Task<User> FindByIdAsync(string userId);
        Task<User> FindByNameAsync(string userName);
        Task<bool> ExistByIdAsync(string userId);
        Task<bool> ExistByNameAsync(string userName);
        Task<User> UpdateAsync(string userId, User user);
        Task<IEnumerable<Role>> RolesAsync(string userId);
        Task<IEnumerable<Role>> RolesAsync(User user);
        Task<string> PasswordResetTokenAsync(string userId);
        Task<User> ResetPasswordAsync(string userId, string password, string token);
        Task<User> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}