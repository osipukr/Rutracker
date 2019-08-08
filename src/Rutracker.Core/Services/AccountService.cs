using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rutracker.Core.Entities.Identity;
using Rutracker.Core.Exceptions;
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
                throw new TorrentException($"Login {userName} is already", ExceptionEventType.NotValidParameters);
            }

            user = new User
            {
                UserName = userName
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new TorrentException($"Not valid parameters: {GetError(result)}.", ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<User> CheckUserAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new TorrentException($"User {userName} does not exist", ExceptionEventType.NotFound);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new TorrentException("Invalid password.", ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task AddUserToRoleAsync(string userId, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                throw new TorrentException($"Role {role} not valid.", ExceptionEventType.NotFound);

            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new TorrentException($"User id {userId} not valid.", ExceptionEventType.NotFound);
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
                throw new TorrentException("Not valid user.", ExceptionEventType.NotFound);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new TorrentException($"Error updating user: {GetError(result)}", ExceptionEventType.NotValidParameters);
            }
        }

        public async Task LogOutUserAsync() => await _signInManager.SignOutAsync();

        private static string GetError(IdentityResult result) => result?.Errors?.FirstOrDefault()?.Description;
    }
}