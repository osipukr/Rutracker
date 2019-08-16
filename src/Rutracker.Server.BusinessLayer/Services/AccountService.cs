using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Services
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

        public async Task<User> CreateAsync(string userName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new TorrentException($"The {nameof(userName)} not valid.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new TorrentException($"The {nameof(email)} not valid.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new TorrentException($"The {nameof(password)} not valid.", ExceptionEventType.NotValidParameters);
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                throw new TorrentException($"User name '{userName}' is already.", ExceptionEventType.NotValidParameters);
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

            await _signInManager.SignInAsync(user, isPersistent: true);

            return user;
        }

        public async Task<User> LoginAsync(string userName, string password, bool rememberMe)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new TorrentException($"The {nameof(userName)} not valid.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new TorrentException($"The {nameof(password)} not valid.", ExceptionEventType.NotValidParameters);
            }

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

            await _signInManager.SignInAsync(user, rememberMe);

            return user;
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    }
}