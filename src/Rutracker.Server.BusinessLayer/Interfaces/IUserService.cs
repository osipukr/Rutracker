using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<User> FindAsync(string userId);
        Task UpdateAsync(User user);
        Task<IEnumerable<string>> RolesAsync(User user);
        Task<User> ChangePasswordAsync(User user, string oldPassword, string newPassword);
        Task<string> EmailConfirmationTokenAsync(User user);
        Task ConfirmEmailAsync(User user, string token);
    }
}