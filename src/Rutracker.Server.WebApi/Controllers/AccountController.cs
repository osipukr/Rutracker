using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models.ViewModels.Accounts;

namespace Rutracker.Server.WebApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountViewModelService _accountViewModelService;
        public AccountController(IAccountViewModelService accountViewModelService)
        {
            _accountViewModelService = accountViewModelService ?? throw new ArgumentNullException(nameof(accountViewModelService));
        }

        [HttpPost(nameof(Login))]
        public async Task<JwtToken> Login(LoginViewModel model) => await _accountViewModelService.LoginAsync(model);

        [HttpPost(nameof(Register))]
        public async Task<JwtToken> Register(RegisterViewModel model) => await _accountViewModelService.RegisterAsync(model);

        [HttpPost(nameof(Logout))]
        public async Task Logout() => await _accountViewModelService.LogoutAsync();
    }
}