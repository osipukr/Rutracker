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
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IJwtFactory _jwtFactory;

        private readonly HostSettings _hostSettings;
        private readonly EmailConfirmationSettings _emailConfirmationSettings;

        public AccountsController(
            IAccountService accountService,
            IUserService userService,
            IEmailService emailService,
            IJwtFactory jwtFactory,
            IOptions<HostSettings> hostOptions,
            IOptions<EmailConfirmationSettings> emailOptions)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _jwtFactory = jwtFactory ?? throw new ArgumentNullException(nameof(jwtFactory));
            _hostSettings = hostOptions?.Value ?? throw new ArgumentNullException(nameof(hostOptions));
            _emailConfirmationSettings = emailOptions?.Value ?? throw new ArgumentNullException(nameof(emailOptions));
        }

        [HttpPost(nameof(Login))]
        public async Task<JwtToken> Login(LoginViewModel model)
        {
            var user = await _accountService.LoginAsync(model.UserName, model.Password, model.RememberMe);
            var roles = await _userService.RolesAsync(user);

            return await _jwtFactory.GenerateTokenAsync(user, roles);
        }

        [HttpPost(nameof(Register))]
        public async Task Register(RegisterViewModel model)
        {
            var user = await _accountService.RegisterAsync(model.UserName, model.Email, model.Password);
            var token = await _userService.EmailConfirmationTokenAsync(user);

            var parameters = HttpUtility.ParseQueryString(string.Empty);

            parameters.Add(nameof(ConfirmEmailViewModel.UserId), user.Id);
            parameters.Add(nameof(ConfirmEmailViewModel.Token), token);

            var urlBuilder = new UriBuilder(_hostSettings.BaseUrl)
            {
                Path = _emailConfirmationSettings.Path,
                Query = parameters.ToString()
            };

            var callbackUrl = urlBuilder.Uri.ToString();

            await _emailService.SendConfirmationEmailAsync(user.Email, callbackUrl);
        }

        [Authorize, HttpPost(nameof(Logout))]
        public async Task Logout()
        {
            await _accountService.LogoutAsync();
        }

        [AllowAnonymous, HttpGet(nameof(ConfirmEmail))]
        public async Task ConfirmEmail([FromQuery] ConfirmEmailViewModel model)
        {
            var user = await _userService.FindAsync(model.UserId);

            await _userService.ConfirmEmailAsync(user, model.Token);
        }
    }
}