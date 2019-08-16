using System;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Server.WebApi.Services
{
    public class AccountViewModelService : IAccountViewModelService
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IJwtFactory _jwtFactory;

        public AccountViewModelService(IAccountService accountService, IUserService userService, IJwtFactory jwtFactory)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _jwtFactory = jwtFactory ?? throw new ArgumentNullException(nameof(jwtFactory));
        }

        public async Task<JwtToken> LoginAsync(LoginViewModel model)
        {
            var user = await _accountService.LoginAsync(model.UserName, model.Password, model.RememberMe);
            var roles = await _userService.RolesAsync(user);

            return await _jwtFactory.GenerateTokenAsync(user, roles);
        }

        public async Task<JwtToken> RegisterAsync(RegisterViewModel model)
        {
            var user = await _accountService.CreateAsync(model.UserName, model.Email, model.Password);
            var roles = await _userService.RolesAsync(user);

            return await _jwtFactory.GenerateTokenAsync(user, roles);
        }

        public async Task LogoutAsync() => await _accountService.LogoutAsync();
    }
}