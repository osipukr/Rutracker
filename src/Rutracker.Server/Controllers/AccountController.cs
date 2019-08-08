using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels.Accounts;

namespace Rutracker.Server.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AccountController : ControllerBase
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

        [HttpPost(nameof(LogOut))]
        public async Task LogOut() => await _accountViewModelService.LogoutAsync();
    }
}