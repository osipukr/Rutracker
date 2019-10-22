using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
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

            if (!user.IsRegistrationFinished)
            {
                throw new RutrackerException("The user has not completed the registration.", ExceptionEventTypes.InvalidParameters);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, rememberMe);

                return user;
            }

            if (result.IsLockedOut)
            {
                throw new RutrackerException("Too many login attempts, try again later.", ExceptionEventTypes.LoginFailed);
            }

            if (result.IsNotAllowed)
            {
                throw new RutrackerException("Confirm your account before logging in.", ExceptionEventTypes.LoginFailed);
            }

            if (result.RequiresTwoFactor)
            {
                // RequiresTwoFactor
            }

            throw new RutrackerException("Wrong password.", ExceptionEventTypes.InvalidParameters);
        }

        public async Task<User> RegisterAsync(string userName, string email)
        {
            Guard.Against.NullOrWhiteSpace(userName, message: "Invalid user name.");
            Guard.Against.NullOrWhiteSpace(email, message: "Invalid email.");

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new User
                {
                    UserName = userName,
                    Email = email,
                    IsRegistrationFinished = false
                };

                var result = await _userManager.CreateAsync(user);

                Guard.Against.IsSucceeded(result);
            }

            if (user.IsRegistrationFinished)
            {
                throw new RutrackerException($"A user with this name '{userName}' is already registered.", ExceptionEventTypes.RegistrationFailed);
            }

            user.Email = email;
            user.RegisteredAt = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return user;
        }

        public async Task<User> CompleteRegistrationAsync(string userId, string token, string firstName, string lastName, string password)
        {
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");
            Guard.Against.NullOrWhiteSpace(token, message: "Invalid confirmation email token.");

            var user = await _userManager.FindByIdAsync(userId);

            Guard.Against.NullNotFound(user, "No user with this id found.");

            if (user.IsRegistrationFinished)
            {
                throw new RutrackerException($"A user with this name '{user.UserName}' is already registered.", ExceptionEventTypes.InvalidParameters);
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            Guard.Against.IsSucceeded(result);

            result = await _userManager.AddPasswordAsync(user, password);

            Guard.Against.IsSucceeded(result);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.RegisteredAt = DateTime.UtcNow;
            user.IsRegistrationFinished = true;

            await _userManager.UpdateAsync(user);
            await _userManager.AddToRoleAsync(user, UserRoles.User);

            return user;
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    }
}