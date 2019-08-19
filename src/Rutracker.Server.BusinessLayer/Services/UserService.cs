using System;
using System.Collections.Generic;
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

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RutrackerException("The user not found.", ExceptionEventType.NotFound);
            }

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }

        public async Task<IEnumerable<string>> RolesAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
            {
                throw new RutrackerException("The roles not found.", ExceptionEventType.NotFound);
            }

            return roles;
        }

        public async Task<string> ChangeEmailTokenAsync(User user, string email)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                throw new RutrackerException("This email is already.", ExceptionEventType.NotValidParameters);
            }

            if (!user.EmailConfirmed)
            {
                throw new RutrackerException("Email not confirmed.", ExceptionEventType.NotValidParameters);
            }

            return await _userManager.GenerateChangeEmailTokenAsync(user, email);
        }

        public async Task<string> ChangePhoneNumberTokenAsync(User user, string phone)
        {
            if (user.PhoneNumber != null && !user.PhoneNumberConfirmed)
            {
                throw new RutrackerException("Phone number not confirmed.", ExceptionEventType.NotValidParameters);
            }

            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phone);
        }

        public async Task<User> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
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
            var result = await _userManager.ChangeEmailAsync(user, email, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<User> ChangePhoneNumberAsync(User user, string phone, string token)
        {
            var result = await _userManager.ChangePhoneNumberAsync(user, phone, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<string> EmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task ConfirmEmailAsync(User user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
            }
        }
    }
}