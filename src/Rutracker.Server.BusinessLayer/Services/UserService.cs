using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IStorageService _storageService;

        public UserService(UserManager<User> userManager, IStorageService storageService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null)
            {
                throw new RutrackerException("The users not found.", ExceptionEventType.NotFound);
            }

            return users;
        }

        public async Task<User> FindAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new RutrackerException("Not valid user id.", ExceptionEventType.NotValidParameters);
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RutrackerException("The user not found.", ExceptionEventType.NotFound);
            }

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }

        public async Task<IEnumerable<string>> RolesAsync(User user)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
            {
                throw new RutrackerException("The roles not found.", ExceptionEventType.NotFound);
            }

            return roles;
        }

        public async Task<string> UploadProfileImageAsync(User user, byte[] imageBytes, string fileType)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            if (imageBytes == null)
            {
                throw new RutrackerException("Not valid image bytes.", ExceptionEventType.NotValidParameters);
            }

            var types = new[] { "image/png", "image/svg", "image/jpeg", "image/gif", "image/jpg" };

            if (string.IsNullOrWhiteSpace(fileType) || !types.Contains(fileType.ToLower()))
            {
                throw new RutrackerException("Not valid file type.", ExceptionEventType.NotValidParameters);
            }

            using var stream = new MemoryStream(imageBytes);

            var containerName = user.UserName;
            var fileName = $"profile-image-{Guid.NewGuid()}.{fileType.Split('/')[1]}";

            await _storageService.UploadFileAsync(containerName, fileName, stream);

            return await _storageService.PathToFileAsync(containerName, fileName);
        }

        public async Task DeleteProfileImageAsync(User user)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            await _storageService.DeleteContainerAsync(user.UserName);
        }

        public async Task<string> ChangeEmailTokenAsync(User user, string email)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new RutrackerException("Not valid email.", ExceptionEventType.NotValidParameters);
            }

            if (await _userManager.FindByEmailAsync(email) != null)
            {
                throw new RutrackerException("This email is already.", ExceptionEventType.NotValidParameters);
            }

            if (user.Email != null && !user.EmailConfirmed)
            {
                throw new RutrackerException("Email not confirmed.", ExceptionEventType.NotValidParameters);
            }

            return await _userManager.GenerateChangeEmailTokenAsync(user, email);
        }

        public async Task<string> ChangePhoneNumberTokenAsync(User user, string phone)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new RutrackerException("Not valid phone.", ExceptionEventType.NotValidParameters);
            }

            if (user.PhoneNumber != null && !user.PhoneNumberConfirmed)
            {
                throw new RutrackerException("Phone number not confirmed.", ExceptionEventType.NotValidParameters);
            }

            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phone);
        }

        public async Task<User> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(oldPassword))
            {
                throw new RutrackerException("Not valid old password.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new RutrackerException("Not valid new password.", ExceptionEventType.NotValidParameters);
            }

            if (!await _userManager.CheckPasswordAsync(user, oldPassword))
            {
                throw new RutrackerException("Old password is not correct.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<User> ChangeEmailAsync(User user, string email, string token)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new RutrackerException("Not valid email.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new RutrackerException("Not valid token.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.ChangeEmailAsync(user, email, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<User> ChangePhoneNumberAsync(User user, string phone, string token)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new RutrackerException("Not valid phone.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new RutrackerException("Not valid token.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.ChangePhoneNumberAsync(user, phone, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<string> EmailConfirmationTokenAsync(User user)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task ConfirmEmailAsync(User user, string token)
        {
            if (user == null)
            {
                throw new RutrackerException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new RutrackerException("Not valid token.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }
    }
}