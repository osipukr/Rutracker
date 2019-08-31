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
        private readonly IStorageService _storageService;

        public UserService(UserManager<User> userManager, IStorageService storageService)
        {
            _userManager = userManager;
            _storageService = storageService;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            Guard.Against.NullNotFound(users, "The users not found.");

            return users;
        }

        public async Task<User> FindAsync(string userId)
        {
            Guard.Against.NullOrWhiteSpace(userId, message: "Not valid user id.");

            var user = await _userManager.FindByIdAsync(userId);

            Guard.Against.NullNotFound(user, "The user not found.");

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            Guard.Against.NullNotValid(user, "Not valid user.");

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }

        public async Task<IEnumerable<string>> RolesAsync(User user)
        {
            Guard.Against.NullNotValid(user, "Not valid user.");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
            {
                throw new RutrackerException("The roles not found.", ExceptionEventType.NotFound);
            }

            return roles;
        }

        public async Task<string> UploadProfileImageAsync(User user, byte[] imageBytes, string fileType)
        {
            Guard.Against.NullNotValid(user, "Not valid user.");
            Guard.Against.NullNotValid(imageBytes, "Not valid image bytes.");

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
            Guard.Against.NullNotValid(user, "Not valid user.");

            await _storageService.DeleteContainerAsync(user.UserName);
        }

        public async Task<string> EmailConfirmationTokenAsync(User user)
        {
            Guard.Against.NullNotValid(user, "Not valid user.");

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> ChangeEmailTokenAsync(User user, string email)
        {
            Guard.Against.NullNotValid(user, "Not valid user.");
            Guard.Against.NullOrWhiteSpace(email, message: "Not valid email.");

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
            Guard.Against.NullNotValid(user, "Not valid user.");
            Guard.Against.NullOrWhiteSpace(phone, message: "Not valid phone.");

            if (user.PhoneNumber != null && !user.PhoneNumberConfirmed)
            {
                throw new RutrackerException("Phone number not confirmed.", ExceptionEventType.NotValidParameters);
            }

            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phone);
        }

        public async Task ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            Guard.Against.NullNotValid(user, "Not valid user.");
            Guard.Against.NullOrWhiteSpace(oldPassword, message: "Not valid old password.");
            Guard.Against.NullOrWhiteSpace(newPassword, message: "Not valid new password.");

            if (!await _userManager.CheckPasswordAsync(user, oldPassword))
            {
                throw new RutrackerException("Old password is not correct.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }

        public async Task ChangeEmailAsync(User user, string email, string token)
        {
            Guard.Against.NullNotValid(user, "Not valid user.");
            Guard.Against.NullOrWhiteSpace(email, message: "Not valid email.");
            Guard.Against.NullOrWhiteSpace(token, message: "Not valid token.");

            var result = await _userManager.ChangeEmailAsync(user, email, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }

        public async Task ChangePhoneNumberAsync(User user, string phone, string token)
        {
            Guard.Against.NullNotValid(user, "Not valid user.");
            Guard.Against.NullOrWhiteSpace(phone, message: "Not valid phone.");
            Guard.Against.NullOrWhiteSpace(token, message: "Not valid token.");

            var result = await _userManager.ChangePhoneNumberAsync(user, phone, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }

        public async Task ConfirmEmailAsync(User user, string token)
        {
            Guard.Against.NullNotValid(user, "Not valid user.");
            Guard.Against.NullOrWhiteSpace(token, message: "Not valid token.");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }
    }
}