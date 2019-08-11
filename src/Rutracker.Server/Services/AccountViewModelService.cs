using System;
using System.Threading.Tasks;
using Rutracker.Core.Interfaces.Services;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels.Accounts;

namespace Rutracker.Server.Services
{
    public class AccountViewModelService : IAccountViewModelService
    {
        private readonly IAccountService _accountService;
        private readonly IJwtFactory _jwtFactory;

        public AccountViewModelService(IAccountService accountService, IJwtFactory jwtFactory)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _jwtFactory = jwtFactory ?? throw new ArgumentNullException(nameof(jwtFactory));
        }

        public async Task<JwtToken> LoginAsync(LoginViewModel model)
        {
            var user = await _accountService.CheckUserAsync(model.UserName, model.Password);
            var roles = await _accountService.GetUserRolesAsync(user);
            var jwt = await _jwtFactory.GenerateTokenAsync(user, roles);

            return jwt;
        }

        public async Task<JwtToken> RegisterAsync(RegisterViewModel model)
        {
            var user = await _accountService.CreateUserAsync(model.UserName, model.Email, model.Password);
            var roles = await _accountService.GetUserRolesAsync(user);
            var jwt = await _jwtFactory.GenerateTokenAsync(user, roles);

            return jwt;
        }

        public async Task LogoutAsync() => await _accountService.LogOutUserAsync();
    }
}