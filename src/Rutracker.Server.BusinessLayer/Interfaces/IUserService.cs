using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IPagedList<User>> ListAsync(IUserFilter filter);
        Task<User> FindAsync(string id);
        Task<User> FindByNameAsync(string userName);
        Task<User> UpdateAsync(string id, User user);
        Task<IEnumerable<string>> RolesAsync(string id);
        Task<User> ChangeImageAsync(string id, string imageUrl);
        Task<User> ChangeImageAsync(string id, IFormFile file);
        Task<User> DeleteImageAsync(string id);
        Task<string> PasswordResetTokenAsync(string id);
        Task<string> ChangeEmailTokenAsync(string id, string email);
        Task<string> ChangePhoneNumberTokenAsync(string id, string phone);
        Task<User> ResetPasswordAsync(string id, string password, string token);
        Task<User> ChangePasswordAsync(string id, string oldPassword, string newPassword);
        Task<User> ChangeEmailAsync(string id, string email, string token);
        Task<User> ChangePhoneNumberAsync(string id, string phone, string token);
    }
}