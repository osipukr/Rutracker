using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models.ViewModels.Account;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The Account API controller.
    /// </summary>
    [AllowAnonymous]
    public class AccountsController : ApiController
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IJwtFactory _jwtFactory;

        public AccountsController(IAccountService accountService, IUserService userService, IJwtFactory jwtFactory)
        {
            _accountService = accountService;
            _userService = userService;
            _jwtFactory = jwtFactory;
        }

        [HttpPost("login")]
        public async Task<string> Login(LoginView model)
        {
            var user = await _accountService.LoginAsync(model.UserName, model.Password, model.RememberMe);
            var roles = await _userService.RolesAsync(user.Id);

            return await _jwtFactory.GenerateTokenAsync(user, roles);
        }

        [HttpPost("register")]
        public async Task<string> Register(RegisterView model)
        {
            var user = await _accountService.RegisterAsync(model.UserName, model.Email, model.Password);
            var roles = await _userService.RolesAsync(user.Id);

            return await _jwtFactory.GenerateTokenAsync(user, roles);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task Logout()
        {
            await _accountService.LogoutAsync();
        }
    }
}