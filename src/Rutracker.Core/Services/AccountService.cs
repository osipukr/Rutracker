using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rutracker.Core.Entities.Identity;
using Rutracker.Core.Exceptions;
using Rutracker.Core.Extensions;
using Rutracker.Core.Interfaces.Services;

namespace Rutracker.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<User> CreateUserAsync(string userName, string email, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                throw new TorrentException($"User with name '{userName}' is already.", ExceptionEventType.NotValidParameters);
            }

            user = new User
            {
                UserName = userName,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new TorrentException(result.GetError(), ExceptionEventType.NotValidParameters);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, UserRoles.Names.User);

            if (!roleResult.Succeeded)
            {
                throw new TorrentException(roleResult.GetError(), ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task<IReadOnlyList<string>> GetUserRolesAsync(User user)
        {
            if (user == null)
            {
                throw new TorrentException("Not valid user.", ExceptionEventType.NotValidParameters);
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
            {
                throw new TorrentException($"The roles for user '{user.UserName}' not found.", ExceptionEventType.NotFound);
            }

            return roles.ToArray();
        }

        public async Task<User> CheckUserAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new TorrentException($"User with name '{userName}' does not exist.", ExceptionEventType.NotFound);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new TorrentException("Not valid password.", ExceptionEventType.NotValidParameters);
            }

            return user;
        }

        public async Task LogOutUserAsync() => await _signInManager.SignOutAsync();
    }
}