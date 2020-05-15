using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Infrastructure.Entities;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly IDateService _dateService;

        public AccountService(SignInManager<User> signInManager, IUserService userService, IDateService dateService)
        {
            _signInManager = signInManager;
            _userService = userService;
            _dateService = dateService;
        }

        public async Task<User> LoginAsync(string userName, string password, bool rememberMe)
        {
            Guard.Against.NullString(userName, Resources.User_InvalidUserName_ErrorMessage);
            Guard.Against.NullString(password, Resources.User_InvalidPassword_ErrorMessage);

            var user = await _userService.FindByNameAsync(userName);

            Guard.Against.NullNotFound(user, string.Format(Resources.User_NotFoundByName_ErrorMessage, userName));

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, rememberMe);

                return user;
            }

            if (result.IsLockedOut)
            {
                throw new RutrackerException(Resources.User_LockedOut_ErrorMessage, ExceptionEventTypes.LoginFailed);
            }

            if (result.IsNotAllowed)
            {
                throw new RutrackerException(Resources.User_NotAllowed_ErrorMessage, ExceptionEventTypes.LoginFailed);
            }

            if (result.RequiresTwoFactor)
            {
            }

            throw new RutrackerException(Resources.User_WrongPassword_ErrorMessage, ExceptionEventTypes.InvalidParameters);
        }

        public async Task<User> RegisterAsync(string userName, string email, string password)
        {
            Guard.Against.NullString(userName, Resources.User_InvalidUserName_ErrorMessage);
            Guard.Against.NullString(email, Resources.User_InvalidEmail_ErrorMessage);

            if (await _userService.ExistByNameAsync(userName))
            {
                throw new RutrackerException(
                    string.Format(Resources.User_AlreadyRegistered_ErrorMessage, userName),
                    ExceptionEventTypes.RegistrationFailed);

            }

            var user = new User
            {
                UserName = userName,
                Email = email,
                AddedDate = _dateService.Now()
            };

            await _userService.AddAsync(user, password);
            await _userService.AddToRoleAsync(user, StockRoles.User);

            return user;
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    }
}