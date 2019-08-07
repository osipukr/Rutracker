using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Rutracker.Core.Entities.Accounts;
using Rutracker.Core.Exceptions;
using Rutracker.Core.Extensions;
using Rutracker.Core.Interfaces.Services;

namespace Rutracker.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<User> CreateUserAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                throw new TorrentException($"Login {userName} is already", ExceptionEventType.RegistrationFailed);
            }

            user = new User
            {
                UserName = userName
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new TorrentException($"Failed to register user with login {userName}.", ExceptionEventType.RegistrationFailed);
            }

            var passwordResult = await _userManager.AddPasswordAsync(user, password);

            if (!passwordResult.Succeeded)
            {
                throw new TorrentException("Not valid password.", ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<User> CheckUserAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            Guard.Against.Null(nameof(user), user);

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new TorrentException("Failed to login user.", ExceptionEventType.LoginFailed);
            }

            return user;
        }

        public async Task AddUserToRoleAsync(string userId, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                throw new TorrentException($"Role {role} not valid.", ExceptionEventType.NotValidParameters);

            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new TorrentException($"User id {userId} not valid.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                throw new TorrentException($"Error adding role {role}.", ExceptionEventType.NotValidParameters);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new TorrentException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new TorrentException("Error updating user.", ExceptionEventType.NotValidParameters);
            }
        }

        public async Task<string> GetEmailConfirmationTokenAsync(User user)
        {
            if (user == null)
            {
                throw new TorrentException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if(string.IsNullOrWhiteSpace(token))
            {
                throw new TorrentException("Not valid token.", ExceptionEventType.NotValidParameters);
            }

            return token;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(User user)
        {
            if (user == null)
            {
                throw new TorrentException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            var roles = await _userManager.GetRolesAsync(user);

            Guard.Against.Null(nameof(roles), roles);

            return roles;
        }

        public async Task LogOutUserAsync() => await _signInManager.SignOutAsync();
    }
}