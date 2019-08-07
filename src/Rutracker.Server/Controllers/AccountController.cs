using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels.Accounts;
using Rutracker.Shared.ViewModels.Accounts.Response;

namespace Rutracker.Server.Controllers
{
    [ApiController, Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public class AccountController : ControllerBase
    {
        private readonly IAccountViewModelService _accountViewModelService;
        public AccountController(IAccountViewModelService accountViewModelService)
        {
            _accountViewModelService = accountViewModelService ?? throw new ArgumentNullException(nameof(accountViewModelService));
        }

        [HttpPost(nameof(Login))]
        public async Task<LoginResponseViewModel> Login(LoginViewModel model) => await _accountViewModelService.LoginAsync(model);

        [HttpPost(nameof(Register))]
        public async Task Register(RegisterViewModel model) => await _accountViewModelService.RegisterAsync(model);

        [HttpGet(nameof(EmailConfirm))]
        public async Task EmailConfirm([FromQuery] ConfirmEmailViewModel model) => await _accountViewModelService.ConfirmRegistrationAsync(model);

        [HttpPost(nameof(LogOut))]
        public async Task LogOut() => await _accountViewModelService.LogoutAsync();
    }
}