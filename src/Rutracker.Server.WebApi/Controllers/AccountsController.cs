using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Server.WebApi.Options;
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
        private readonly IEmailService _emailService;

        private readonly ClientOptions _clientOptions;

        public AccountsController(
            IAccountService accountService,
            IUserService userService,
            IJwtFactory jwtFactory,
            IEmailService emailService,
            IOptions<ClientOptions> options)
        {
            _accountService = accountService;
            _userService = userService;
            _jwtFactory = jwtFactory;
            _emailService = emailService;

            _clientOptions = options.Value;
        }

        [HttpPost("login")]
        public async Task<TokenView> Login(LoginView model)
        {
            var user = await _accountService.LoginAsync(model.UserName, model.Password, model.RememberMe);
            var roles = await _userService.RolesAsync(user);

            return await _jwtFactory.GenerateTokenAsync(user, roles);
        }

        [HttpPost("register")]
        public async Task<TokenView> Register(RegisterView model)
        {
            var user = await _accountService.RegisterAsync(model.UserName, model.Email, model.Password);
            var roles = await _userService.RolesAsync(user);

            return await _jwtFactory.GenerateTokenAsync(user, roles);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task Logout()
        {
            await _accountService.LogoutAsync();
        }

        [HttpPost("forgotPassword")]
        public async Task ForgotPassword(ForgotPasswordView model)
        {
            var user = await _userService.FindByNameAsync(model.UserName);
            var token = await _userService.PasswordResetTokenAsync(user.Id);

            var clientPath = new Uri(new Uri(_clientOptions.BaseUrl), _clientOptions.ResetPasswordPath);
            var resetUrl = QueryHelpers.AddQueryString(clientPath.AbsoluteUri,
                new Dictionary<string, string>
                {
                    [nameof(ResetPasswordView.UserId)] = user.Id,
                    [nameof(ResetPasswordView.Token)] = token
                });

            await _emailService.SendResetPasswordAsync(user.Email, resetUrl);
        }

        [HttpPost("resetPassword")]
        public async Task ResetPassword(ResetPasswordView model)
        {
            await _userService.ResetPasswordAsync(model.UserId, model.Password, model.Token);
        }
    }
}