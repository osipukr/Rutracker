using System;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models.ViewModels.Accounts;

namespace Rutracker.Server.WebApi.Services
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
            var user = await _accountService.LoginAsync(model.UserName, model.Password);
            var roles = await _accountService.RolesAsync(user);
            var jwt = await _jwtFactory.GenerateTokenAsync(user, roles);

            return jwt;
        }

        public async Task<JwtToken> RegisterAsync(RegisterViewModel model)
        {
            var user = await _accountService.CreateAsync(model.UserName, model.Email, model.Password);
            var roles = await _accountService.RolesAsync(user);
            var jwt = await _jwtFactory.GenerateTokenAsync(user, roles);

            return jwt;
        }

        public async Task LogoutAsync() => await _accountService.LogoutAsync();
    }
}