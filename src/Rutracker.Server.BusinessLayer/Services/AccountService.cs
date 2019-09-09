using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Entities;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> LoginAsync(string userName, string password, bool rememberMe)
        {
            Guard.Against.NullOrWhiteSpace(userName, message: "Invalid user name.");
            Guard.Against.NullOrWhiteSpace(password, message: "Invalid password.");

            var user = await _userManager.FindByNameAsync(userName);

            Guard.Against.NullNotFound(user, $"The user with name '{userName}' not found.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, rememberMe);

                return user;
            }

            if (result.IsLockedOut)
            {
                throw new RutrackerException("Too many login attempts, try again later.", ExceptionEventType.NotValidParameters);
            }

            if (result.IsNotAllowed)
            {
                throw new RutrackerException("Confirm your account before logging in.", ExceptionEventType.NotValidParameters);
            }

            if (result.RequiresTwoFactor)
            {
                // RequiresTwoFactor
            }

            throw new RutrackerException("Wrong password.", ExceptionEventType.NotValidParameters);
        }

        public async Task<User> RegisterAsync(string userName, string email, string password)
        {
            Guard.Against.NullOrWhiteSpace(userName, message: "Invalid user name.");
            Guard.Against.NullOrWhiteSpace(email, message: "Invalid email.");
            Guard.Against.NullOrWhiteSpace(password, message: "Invalid password.");

            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                throw new RutrackerException($"The name '{userName}' is already.", ExceptionEventType.NotValidParameters);
            }

            user = new User
            {
                UserName = userName,
                Email = email,
                RegisteredAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, UserRoles.User);

                if (result.Succeeded)
                {
                    return user;
                }
            }

            throw new RutrackerException(result.GetError(), ExceptionEventType.NotValidParameters);
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    }
}