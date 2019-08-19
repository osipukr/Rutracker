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
        Task<string> ChangeEmailTokenAsync(User user, string email);
        Task<string> ChangePhoneNumberTokenAsync(User user, string phone);
        Task<User> ChangePasswordAsync(User user, string oldPassword, string newPassword);
        Task<User> ChangeEmailAsync(User user, string email, string token);
        Task<User> ChangePhoneNumberAsync(User user, string phone, string token);
        Task<string> EmailConfirmationTokenAsync(User user);
        Task ConfirmEmailAsync(User user, string token);
    }
}