using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.Extensions.Options;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Server.WebApi.Settings;
using Rutracker.Shared.Infrastructure.Exceptions;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Services
{
    public class UserViewModelService : IUserViewModelService
    {
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IMapper _mapper;

        private readonly HostSettings _hostSettings;
        private readonly EmailChangeConfirmationSettings _emailChangeConfirmationSettings;

        public UserViewModelService(
            IUserService userService,
            IStorageService storageService,
            IEmailService emailService,
            ISmsService smsService,
            IMapper mapper,
            IOptions<HostSettings> hostOptions,
            IOptions<EmailChangeConfirmationSettings> emailOptions)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _hostSettings = hostOptions?.Value ?? throw new ArgumentNullException(nameof(hostOptions));
            _emailChangeConfirmationSettings = emailOptions?.Value ?? throw new ArgumentNullException(nameof(emailOptions));
        }

        public async Task<UserViewModel[]> UsersAsync()
        {
            var users = await _userService.ListAsync();

            return _mapper.Map<UserViewModel[]>(users);
        }

        public async Task<UserDetailsViewModel> UserAsync(ClaimsPrincipal principal)
        {
            var user = await _userService.FindAsync(principal.GetUserId());
            var roles = await _userService.RolesAsync(user);
            var userResult = _mapper.Map<UserDetailsViewModel>(user);

            userResult.Roles = roles.ToArray();

            return userResult;
        }

        public async Task ChangeUserAsync(ClaimsPrincipal principal, ChangeUserViewModel model)
        {
            var user = await _userService.FindAsync(principal.GetUserId());

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _userService.UpdateAsync(user);
        }

        public async Task ChangeImageAsync(ClaimsPrincipal principal, ChangeImageViewModel model)
        {
            var user = await _userService.FindAsync(principal.GetUserId());

            if (model.ImageBytes?.Length > 0)
            {
                ThrowIfInvalidFileType(model.FileType);

                await using var stream = new MemoryStream(model.ImageBytes);

                var containerName = user.UserName;
                var fileType = model.FileType.Split('/')[1];
                var fileName = $"profile-image-{Guid.NewGuid()}.{fileType}";

                await _storageService.UploadFileAsync(containerName, fileName, stream);

                user.ImageUrl = await _storageService.PathToFileAsync(containerName, fileName);
            }
            else
            {
                user.ImageUrl = model.ImageUrl;
            }

            await _userService.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(ClaimsPrincipal principal, ChangePasswordViewModel model)
        {
            var user = await _userService.FindAsync(principal.GetUserId());

            await _userService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        }

        public async Task ChangeEmailAsync(ClaimsPrincipal principal, ChangeEmailViewModel model)
        {
            var userId = principal.GetUserId();
            var user = await _userService.FindAsync(userId);
            var token = await _userService.ChangeEmailTokenAsync(user, model.Email);

            var parameters = HttpUtility.ParseQueryString(string.Empty);

            parameters.Add(nameof(ConfirmChangeEmailViewModel.UserId), userId);
            parameters.Add(nameof(ConfirmChangeEmailViewModel.Email), model.Email);
            parameters.Add(nameof(ConfirmChangeEmailViewModel.Token), token);

            var urlBuilder = new UriBuilder(_hostSettings.BaseUrl)
            {
                Path = _emailChangeConfirmationSettings.Path,
                Query = parameters.ToString()
            };

            var callbackUrl = urlBuilder.Uri.ToString();

            await _emailService.SendEmailChangeConfirmation(user.Email, callbackUrl);
        }

        public async Task ChangePhoneNumberAsync(ClaimsPrincipal principal, ChangePhoneNumberViewModel model)
        {
            var user = await _userService.FindAsync(principal.GetUserId());
            var token = await _userService.ChangePhoneNumberTokenAsync(user, model.PhoneNumber);
            var phone = user.PhoneNumber ?? model.PhoneNumber;

            await _smsService.SendConfirmationPhoneAsync(phone, token);
        }

        public async Task DeleteImageAsync(ClaimsPrincipal principal)
        {
            var user = await _userService.FindAsync(principal.GetUserId());

            await _storageService.DeleteContainerAsync(user.UserName);

            user.ImageUrl = null;

            await _userService.UpdateAsync(user);
        }

        public async Task ConfirmChangeEmailAsync(ConfirmChangeEmailViewModel model)
        {
            var user = await _userService.FindAsync(model.UserId);

            await _userService.ChangeEmailAsync(user, model.Email, model.Token);
        }


        public async Task ConfirmChangePhoneNumber(ClaimsPrincipal principal, ConfirmChangePhoneNumberViewModel model)
        {
            var user = await _userService.FindAsync(principal.GetUserId());

            await _userService.ChangePhoneNumberAsync(user, model.Phone, model.Token);
        }

        private static void ThrowIfInvalidFileType(string fileType)
        {
            var types = new[] { "image/png", "image/svg", "image/jpeg", "image/gif", "image/jpg" };
            var type = fileType.ToLower();

            if (!types.Contains(type))
            {
                throw new RutrackerException("Invalid file type.", ExceptionEventType.NotValidParameters);
            }
        }
    }
}