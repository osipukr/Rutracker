using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Server.WebApi.Settings;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The User API controller.
    /// </summary>
    /// <response code="400">If the parameters are not valid.</response>
    /// <response code="401">If the user is not authorized.</response>
    /// <response code="404">If the item is null.</response>
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IMapper _mapper;

        private readonly HostSettings _hostSettings;
        private readonly EmailChangeConfirmationSettings _emailChangeConfirmationSettings;

        public UsersController(
            IUserService userService,
            IEmailService emailService,
            ISmsService smsService,
            IMapper mapper,
            IOptions<HostSettings> hostOptions,
            IOptions<EmailChangeConfirmationSettings> emailOptions)
        {
            _userService = userService;
            _emailService = emailService;
            _smsService = smsService;
            _mapper = mapper;
            _hostSettings = hostOptions.Value;
            _emailChangeConfirmationSettings = emailOptions.Value;
        }

        [HttpGet]
        public async Task<UserViewModel[]> GetAll()
        {
            var users = await _userService.ListAsync();

            return _mapper.Map<UserViewModel[]>(users);
        }

        [HttpGet(nameof(Details))]
        public async Task<UserViewModel> Details()
        {
            var user = await _userService.FindAsync(User.GetUserId());
            var roles = await _userService.RolesAsync(user);
            var userResult = _mapper.Map<UserDetailsViewModel>(user);

            userResult.Roles = roles.ToArray();

            return userResult;
        }

        [HttpPut(nameof(ChangeUser))]
        public async Task ChangeUser(ChangeUserViewModel model)
        {
            var user = await _userService.FindAsync(User.GetUserId());

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _userService.UpdateAsync(user);
        }

        [HttpPut(nameof(ChangeImage))]
        public async Task ChangeImage(ChangeImageViewModel model)
        {
            var user = await _userService.FindAsync(User.GetUserId());

            if (model.ImageBytes?.Length > 0)
            {
                var path = await _userService.UploadProfileImageAsync(user, model.ImageBytes, model.FileType);

                user.ImageUrl = path;
            }
            else
            {
                user.ImageUrl = model.ImageUrl;
            }

            await _userService.UpdateAsync(user);
        }

        [HttpPut(nameof(ChangePassword))]
        public async Task ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userService.FindAsync(User.GetUserId());

            await _userService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        }

        [HttpPut(nameof(ChangeEmail))]
        public async Task ChangeEmail(ChangeEmailViewModel model)
        {
            var userId = User.GetUserId();
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

        [HttpPut(nameof(ChangePhoneNumber))]
        public async Task ChangePhoneNumber(ChangePhoneNumberViewModel model)
        {
            var user = await _userService.FindAsync(User.GetUserId());
            var token = await _userService.ChangePhoneNumberTokenAsync(user, model.PhoneNumber);
            var phone = user.PhoneNumber ?? model.PhoneNumber;

            await _smsService.SendConfirmationPhoneAsync(phone, token);
        }

        [HttpDelete(nameof(DeleteImage))]
        public async Task DeleteImage()
        {
            var user = await _userService.FindAsync(User.GetUserId());

            await _userService.DeleteProfileImageAsync(user);

            user.ImageUrl = null;

            await _userService.UpdateAsync(user);
        }

        [AllowAnonymous, HttpGet(nameof(ConfirmChangeEmail))]
        public async Task ConfirmChangeEmail([FromQuery] ConfirmChangeEmailViewModel model)
        {
            var user = await _userService.FindAsync(model.UserId);

            await _userService.ChangeEmailAsync(user, model.Email, model.Token);
        }

        [HttpPost(nameof(ConfirmChangePhoneNumber))]
        public async Task ConfirmChangePhoneNumber(ConfirmChangePhoneNumberViewModel model)
        {
            var user = await _userService.FindAsync(User.GetUserId());

            await _userService.ChangePhoneNumberAsync(user, model.Phone, model.Token);
        }
    }
}