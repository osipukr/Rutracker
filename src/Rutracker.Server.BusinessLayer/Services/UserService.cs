using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IFileStorageService _fileStorageService;

        public UserService(UserManager<User> userManager, IFileStorageService fileStorageService)
        {
            _userManager = userManager;
            _fileStorageService = fileStorageService;
        }

        public async Task<Tuple<IEnumerable<User>, int>> ListAsync(int page, int pageSize)
        {
            Guard.Against.LessOne(page, $"The {nameof(page)} is less than 1.");
            Guard.Against.OutOfRange(pageSize, rangeFrom: 1, rangeTo: 100, $"The {nameof(pageSize)} is out of range ({1} - {100}).");

            var query = _userManager.Users.OrderBy(x => x.RegisteredAt);
            var users = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            Guard.Against.NullNotFound(users, "The users not found.");

            var count = await query.CountAsync();

            return Tuple.Create<IEnumerable<User>, int>(users, count);
        }

        public async Task<User> FindAsync(string id)
        {
            Guard.Against.NullOrWhiteSpace(id, message: "Invalid user id.");

            var user = await _userManager.FindByIdAsync(id);

            Guard.Against.NullNotFound(user, $"The user with id '{id}' not found.");

            return user;
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            Guard.Against.NullOrWhiteSpace(userName, message: "Invalid user name.");

            var user = await _userManager.FindByNameAsync(userName);

            Guard.Against.NullNotFound(user, $"The user with name '{userName}' not found.");

            return user;
        }

        public async Task<User> UpdateAsync(string id, User user)
        {
            Guard.Against.NullOrWhiteSpace(id, message: "Invalid user id.");
            Guard.Against.NullNotValid(user, "Invalid user model.");

            var result = await FindAsync(id);

            result.FirstName = user.FirstName;
            result.LastName = user.LastName;

            var identityResult = await _userManager.UpdateAsync(result);

            if (!identityResult.Succeeded)
            {
                throw new RutrackerException(identityResult.GetError(), ExceptionEventTypes.NotValidParameters);
            }

            return result;
        }

        public async Task<IEnumerable<string>> RolesAsync(string id)
        {
            var user = await FindAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
            {
                throw new RutrackerException("The roles not found.", ExceptionEventTypes.NotFound);
            }

            return roles;
        }

        public async Task<User> ChangeImageAsync(string id, string imageUrl)
        {
            var user = await FindAsync(id);

            user.ImageUrl = imageUrl;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventTypes.NotValidParameters);
            }

            return user;
        }

        public async Task<User> ChangeImageAsync(string id, string mimeType, Stream imageStream)
        {
            var path = await _fileStorageService.UploadUserImageAsync(id, mimeType, imageStream);

            return await ChangeImageAsync(id, path);
        }

        public async Task<User> DeleteImageAsync(string id)
        {
            return await ChangeImageAsync(id, null);
        }

        public async Task<string> EmailConfirmationTokenAsync(string id)
        {
            var user = await FindAsync(id);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            Guard.Against.NullOrWhiteSpace(token, message: "Invalid token.");

            return token;
        }

        public async Task<string> ChangeEmailTokenAsync(string id, string email)
        {
            Guard.Against.NullOrWhiteSpace(email, message: "Invalid email.");

            var user = await FindAsync(id);

            if (await _userManager.FindByEmailAsync(email) != null)
            {
                throw new RutrackerException("This email is already.", ExceptionEventTypes.NotValidParameters);
            }

            if (!string.IsNullOrWhiteSpace(user.Email) && !user.EmailConfirmed)
            {
                throw new RutrackerException("The email is not confirmed.", ExceptionEventTypes.NotValidParameters);
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, email);

            Guard.Against.NullOrWhiteSpace(token, message: "Invalid token.");

            return token;
        }

        public async Task<string> ChangePhoneNumberTokenAsync(string id, string phone)
        {
            Guard.Against.NullOrWhiteSpace(phone, message: "Invalid phone number.");

            var user = await FindAsync(id);

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && !user.PhoneNumberConfirmed)
            {
                throw new RutrackerException("The phone number is not confirmed.", ExceptionEventTypes.NotValidParameters);
            }

            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phone);

            Guard.Against.NullOrWhiteSpace(token, message: "Invalid token.");

            return token;
        }

        public async Task<User> ChangePasswordAsync(string id, string oldPassword, string newPassword)
        {
            Guard.Against.NullOrWhiteSpace(oldPassword, message: "Invalid old password.");
            Guard.Against.NullOrWhiteSpace(newPassword, message: "Invalid new password.");

            var user = await FindAsync(id);

            if (!await _userManager.CheckPasswordAsync(user, oldPassword))
            {
                throw new RutrackerException("The old password is not correct.", ExceptionEventTypes.NotValidParameters);
            }

            if (oldPassword == newPassword)
            {
                throw new RutrackerException("The new password must not match the old password.", ExceptionEventTypes.NotValidParameters);
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventTypes.NotValidParameters);
            }

            return user;
        }

        public async Task<User> ChangeEmailAsync(string id, string email, string token)
        {
            Guard.Against.NullOrWhiteSpace(email, message: "Invalid email.");
            Guard.Against.NullOrWhiteSpace(token, message: "Invalid token.");

            var user = await FindAsync(id);
            var result = await _userManager.ChangeEmailAsync(user, email, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventTypes.NotValidParameters);
            }

            return user;
        }

        public async Task<User> ChangePhoneNumberAsync(string id, string phone, string token)
        {
            Guard.Against.NullOrWhiteSpace(phone, message: "Invalid phone.");
            Guard.Against.NullOrWhiteSpace(token, message: "Invalid token.");

            var user = await FindAsync(id);
            var result = await _userManager.ChangePhoneNumberAsync(user, phone, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventTypes.NotValidParameters);
            }

            return user;
        }
    }
}