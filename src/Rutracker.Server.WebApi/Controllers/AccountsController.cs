using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The Account API controller.
    /// </summary>
    /// <response code="400">If the parameters are not valid.</response>
    /// <response code="404">If the item is null.</response>
    public class AccountsController : BaseApiController
    {
        private readonly IAccountViewModelService _accountViewModelService;
        public AccountsController(IAccountViewModelService accountViewModelService)
        {
            _accountViewModelService = accountViewModelService ?? throw new ArgumentNullException(nameof(accountViewModelService));
        }

        [HttpPost(nameof(Login))]
        public async Task<JwtToken> Login(LoginViewModel model) => await _accountViewModelService.LoginAsync(model);

        [HttpPost(nameof(Register))]
        public async Task<JwtToken> Register(RegisterViewModel model) => await _accountViewModelService.RegisterAsync(model);

        [Authorize]
        [HttpPost(nameof(Logout))]
        public async Task Logout() => await _accountViewModelService.LogoutAsync();
    }
}