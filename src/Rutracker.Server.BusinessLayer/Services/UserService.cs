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

        public async Task<User> FindAsync(string id)
        {
            Guard.Against.NullOrWhiteSpace(id, message: "Not valid user id.");

            var user = await _userManager.FindByIdAsync(id);

            Guard.Against.NullNotFound(user, $"The user with id '{id}' not found.");

            return user;
        }

        public async Task<User> UpdateAsync(string id, User user)
        {
            Guard.Against.NullOrWhiteSpace(id, message: "Not valid user id.");
            Guard.Against.NullNotValid(user, "Not valid user.");

            var result = await FindAsync(id);

            result.FirstName = user.FirstName;
            result.LastName = user.LastName;

            var identityResult = await _userManager.UpdateAsync(result);

            if (!identityResult.Succeeded)
            {
                throw new RutrackerException(identityResult.GetError(), ExceptionEventType.NotValidParameters);
            }

            return result;
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

        public async Task<User> ChangeImageAsync(string id, string imageUrl)
        {
            var user = await FindAsync(id);

            user.ImageUrl = imageUrl;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<User> ChangeImageAsync(string id, byte[] imageBytes, string fileType)
        {
            Guard.Against.NullNotValid(imageBytes, "Not valid image bytes.");

            var types = new[] { "image/png", "image/svg", "image/jpeg", "image/gif", "image/jpg" };

            if (string.IsNullOrWhiteSpace(fileType) || !types.Contains(fileType.ToLower()))
            {
                throw new RutrackerException("Not valid file type.", ExceptionEventType.NotValidParameters);
            }

            await using var stream = new MemoryStream(imageBytes);

            var containerName = id;
            var fileName = $"profile-image-{Guid.NewGuid()}.{fileType.Split('/')[1]}";
            var path = await _storageService.UploadFileAsync(containerName, fileName, stream);

            return await ChangeImageAsync(id, path);
        }

        public async Task<User> DeleteImageAsync(string id)
        {
            await _storageService.DeleteContainerAsync(id);

            return await ChangeImageAsync(id, null);
        }

        public async Task<string> EmailConfirmationTokenAsync(string id)
        {
            var user = await FindAsync(id);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            Guard.Against.NullOrWhiteSpace(token, message: "Not valid token.");

            return token;
        }

        public async Task<string> ChangeEmailTokenAsync(string id, string email)
        {
            Guard.Against.NullOrWhiteSpace(email, message: "Not valid email.");

            var user = await FindAsync(id);

            if (await _userManager.FindByEmailAsync(email) != null)
            {
                throw new RutrackerException("This email is already.", ExceptionEventType.NotValidParameters);
            }

            if (!string.IsNullOrWhiteSpace(user.Email) && !user.EmailConfirmed)
            {
                throw new RutrackerException("Email not confirmed.", ExceptionEventType.NotValidParameters);
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, email);

            Guard.Against.NullOrWhiteSpace(token, message: "Not valid token.");

            return token;
        }

        public async Task<string> ChangePhoneNumberTokenAsync(string id, string phone)
        {
            Guard.Against.NullOrWhiteSpace(phone, message: "Not valid phone.");

            var user = await FindAsync(id);

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && !user.PhoneNumberConfirmed)
            {
                throw new RutrackerException("Phone number not confirmed.", ExceptionEventType.NotValidParameters);
            }

            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phone);

            Guard.Against.NullOrWhiteSpace(token, message: "Not valid token.");

            return token;
        }

        public async Task<User> ChangePasswordAsync(string id, string oldPassword, string newPassword)
        {
            Guard.Against.NullOrWhiteSpace(oldPassword, message: "Not valid old password.");
            Guard.Against.NullOrWhiteSpace(newPassword, message: "Not valid new password.");

            var user = await FindAsync(id);

            if (!await _userManager.CheckPasswordAsync(user, oldPassword))
            {
                throw new RutrackerException("Old password is not correct.", ExceptionEventType.NotValidParameters);
            }

            if (oldPassword == newPassword)
            {
                throw new RutrackerException("The new password must not match the old password.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<User> ChangeEmailAsync(string id, string email, string token)
        {
            Guard.Against.NullOrWhiteSpace(email, message: "Not valid email.");
            Guard.Against.NullOrWhiteSpace(token, message: "Not valid token.");

            var user = await FindAsync(id);
            var result = await _userManager.ChangeEmailAsync(user, email, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<User> ChangePhoneNumberAsync(string id, string phone, string token)
        {
            Guard.Against.NullOrWhiteSpace(phone, message: "Not valid phone.");
            Guard.Against.NullOrWhiteSpace(token, message: "Not valid token.");

            var user = await FindAsync(id);
            var result = await _userManager.ChangePhoneNumberAsync(user, phone, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<User> ConfirmEmailAsync(string id, string token)
        {
            Guard.Against.NullOrWhiteSpace(token, message: "Not valid token.");

            var user = await FindAsync(id);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }
    }
}