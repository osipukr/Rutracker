using System;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.Extensions.Options;
using Rutracker.Core.Entities.Accounts;
using Rutracker.Core.Interfaces.Services;
using Rutracker.Server.Interfaces;
using Rutracker.Server.Settings;
using Rutracker.Shared.ViewModels.Accounts;
using Rutracker.Shared.ViewModels.Accounts.Response;

namespace Rutracker.Server.Services
{
    public class AccountViewModelService : IAccountViewModelService
    {
        private readonly IAccountService _accountService;
        private readonly IEmailConfirmationService _emailConfirmationService;
        private readonly IEmailService _emailService;
        private readonly HostSettings _hostSettings;
        private readonly EmailConfirmationSettings _emailConfirmationSettings;
        private readonly IMapper _mapper;
        private readonly IJwtFactory _jwtFactory;

        public AccountViewModelService(IAccountService accountService,
            IEmailConfirmationService emailConfirmationService,
            IEmailService emailService,
            IOptions<HostSettings> hostOptions,
            IOptions<EmailConfirmationSettings> emailConfirmationOptions,
            IMapper mapper,
            IJwtFactory jwtFactory)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _emailConfirmationService = emailConfirmationService ?? throw new ArgumentNullException(nameof(emailConfirmationService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _hostSettings = hostOptions?.Value ?? throw new ArgumentNullException(nameof(hostOptions));
            _emailConfirmationSettings = emailConfirmationOptions?.Value ?? throw new ArgumentNullException(nameof(emailConfirmationOptions));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwtFactory = jwtFactory ?? throw new ArgumentNullException(nameof(jwtFactory));
        }

        public async Task<LoginResponseViewModel> LoginAsync(LoginViewModel model)
        {
            var user = await _accountService.CheckUserAsync(model.UserName, model.Password);
            var jwt = await _jwtFactory.GenerateTokenAsync(user);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            var roles = await _accountService.GetUserRolesAsync(user);

            return new LoginResponseViewModel(userViewModel, roles)
            {
                Token = jwt.Token,
                ExpiresIn = jwt.ExpiresIn
            };
        }

        public async Task RegisterAsync(RegisterViewModel model)
        {
            var user = await _accountService.CreateUserAsync(model.UserName, model.Email, model.Password);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _accountService.UpdateUserAsync(user);

            var code = await _accountService.GetEmailConfirmationTokenAsync(user);
            var callbackUrl = GenerateCallbackUrl(user.Id, code);

            await _emailService.SendConfirmationEmailAsync(model.Email, callbackUrl);
        }

        public async Task ConfirmRegistrationAsync(ConfirmEmailViewModel model)
        {
            await _emailConfirmationService.ConfirmEmailAsync(model.UserId, model.Code);
            await _accountService.AddUserToRoleAsync(model.UserId, UserRoles.User);
        }

        public async Task LogoutAsync() => await _accountService.LogOutUserAsync();

        private string GenerateCallbackUrl(string userId, string code)
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters.Add(nameof(ConfirmEmailViewModel.UserId), userId);
            parameters.Add(nameof(ConfirmEmailViewModel.Code), code);

            var urlBuilder = new UriBuilder(_hostSettings.BaseUrl)
            {
                Path = _emailConfirmationSettings.Path,
                Query = parameters.ToString()
            };

            return urlBuilder.Uri.ToString();
        }
    }
}