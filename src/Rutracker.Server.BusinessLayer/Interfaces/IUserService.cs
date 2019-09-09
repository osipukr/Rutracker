using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<Tuple<IEnumerable<User>, int>> ListAsync(int page, int pageSize);
        Task<User> FindAsync(string id);
        Task<User> UpdateAsync(string id, User user);
        Task<IEnumerable<string>> RolesAsync(string id);
        Task<User> ChangeImageAsync(string id, string imageUrl);
        Task<User> ChangeImageAsync(string id, byte[] imageBytes, string imageType);
        Task<User> DeleteImageAsync(string id);
        Task<string> EmailConfirmationTokenAsync(string id);
        Task<string> ChangeEmailTokenAsync(string id, string email);
        Task<string> ChangePhoneNumberTokenAsync(string id, string phone);
        Task<User> ChangePasswordAsync(string id, string oldPassword, string newPassword);
        Task<User> ChangeEmailAsync(string id, string email, string token);
        Task<User> ChangePhoneNumberAsync(string id, string phone, string token);
        Task<User> ConfirmEmailAsync(string id, string token);
    }
}