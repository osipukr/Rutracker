using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Server.WebApi.Settings;
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
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IJwtFactory _jwtFactory;
        private readonly ClientSettings _clientSettings;

        public AccountsController(
            IAccountService accountService,
            IUserService userService,
            IEmailService emailService,
            IJwtFactory jwtFactory,
            IOptions<ClientSettings> clientOptions) : base(null)
        {
            _accountService = accountService;
            _userService = userService;
            _emailService = emailService;
            _jwtFactory = jwtFactory;
            _clientSettings = clientOptions.Value;
        }

        [HttpPost("login")]
        public async Task<string> Login(LoginViewModel model)
        {
            var user = await _accountService.LoginAsync(model.UserName, model.Password, model.RememberMe);
            var roles = await _userService.RolesAsync(user.Id);

            return await _jwtFactory.GenerateTokenAsync(user, roles);
        }

        [HttpPost("register")]
        public async Task Register(RegisterViewModel model)
        {
            var user = await _accountService.RegisterAsync(model.UserName, model.Email);
            var token = await _userService.EmailConfirmationTokenAsync(user.Id);
            var parameters = HttpUtility.ParseQueryString(string.Empty);

            parameters.Add(nameof(CompleteRegistrationViewModel.UserId), user.Id);
            parameters.Add(nameof(CompleteRegistrationViewModel.Token), token);

            var urlBuilder = new UriBuilder(_clientSettings.BaseUrl)
            {
                Path = _clientSettings.CompleteRegistrationPath,
                Query = parameters.ToString()
            };

            await _emailService.SendConfirmationEmailAsync(user.Email, urlBuilder.Uri.ToString());
        }

        [HttpPost("completeRegistration")]
        public async Task<string> CompleteRegistration(CompleteRegistrationViewModel model)
        {
            var user = await _accountService.CompleteRegistrationAsync(model.UserId, model.Token, model.FirstName, model.LastName, model.Password);
            var roles = await _userService.RolesAsync(user.Id);

            return await _jwtFactory.GenerateTokenAsync(user, roles);
        }

        [HttpPost("forgotPassword")]
        public async Task ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userService.FindByNameAsync(model.UserName);
            var token = await _userService.PasswordResetTokenAsync(user.Id);
            var parameters = HttpUtility.ParseQueryString(string.Empty);

            parameters.Add(nameof(ResetPasswordViewModel.UserId), user.Id);
            parameters.Add(nameof(ResetPasswordViewModel.Token), token);

            var urlBuilder = new UriBuilder(_clientSettings.BaseUrl)
            {
                Path = _clientSettings.ResetPasswordPath,
                Query = parameters.ToString()
            };

            await _emailService.SendResetPasswordAsync(user.Email, urlBuilder.Uri.ToString());
        }

        [HttpPost("resetPassword")]
        public async Task ResetPassword(ResetPasswordViewModel model)
        {
            await _userService.ResetPasswordAsync(model.UserId, model.Password, model.Token);
        }

        [Authorize, HttpPost("logout")]
        public async Task Logout()
        {
            await _accountService.LogoutAsync();
        }
    }
}