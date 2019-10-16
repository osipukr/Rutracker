using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Server.WebApi.Settings;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The User API controller.
    /// </summary>
    /// <response code="400">If the parameters are not valid.</response>
    /// <response code="401">If the user is not authorized.</response>
    /// <response code="404">If the item is null.</response>
    [Authorize(Policy = Policies.IsUser)]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        private readonly ClientSettings _clientSettings;

        public UsersController(
            IUserService userService,
            IEmailService emailService,
            ISmsService smsService,
            IMapper mapper,
            IOptions<ClientSettings> clientOptions) : base(mapper)
        {
            _userService = userService;
            _emailService = emailService;
            _smsService = smsService;
            _clientSettings = clientOptions.Value;
        }

        [HttpGet("search"), AllowAnonymous]
        public async Task<PaginationResult<UserViewModel>> Search(int page, int pageSize)
        {
            var (users, count) = await _userService.ListAsync(page, pageSize);

            return new PaginationResult<UserViewModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = count,
                Items = _mapper.Map<IEnumerable<UserViewModel>>(users)
            };
        }

        [HttpGet("find")]
        public async Task<IEnumerable<UserViewModel>> Find(string search)
        {
            var users = await _userService.ListAsync(search);

            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        [HttpGet("profile/{username}"), AllowAnonymous]
        public async Task<UserProfileViewModel> Profile(string userName)
        {
            var user = await _userService.FindByNameAsync(userName);

            return _mapper.Map<UserProfileViewModel>(user);
        }

        [HttpGet("details")]
        public async Task<UserDetailsViewModel> Details()
        {
            var userId = User.GetUserId();
            var user = await _userService.FindAsync(userId);
            var result = _mapper.Map<UserDetailsViewModel>(user);

            result.Roles = await _userService.RolesAsync(userId);

            return result;
        }

        [HttpPut("change/info")]
        public async Task<UserDetailsViewModel> ChangeInfo(ChangeUserViewModel model)
        {
            var userId = User.GetUserId();
            var user = _mapper.Map<User>(model);
            var result = await _userService.UpdateAsync(userId, user);

            return _mapper.Map<UserDetailsViewModel>(result);
        }

        [HttpPut("change/image")]
        public async Task<string> ChangeImage(ChangeImageViewModel model)
        {
            var userId = User.GetUserId();
            var user = await _userService.ChangeImageAsync(userId, model.ImageUrl);

            return user.ImageUrl;
        }

        [HttpPost("change/image")]
        public async Task<string> ChangeImage([FromForm] ChangeImageFileViewModel model)
        {
            var userId = User.GetUserId();
            var user = await _userService.ChangeImageAsync(userId, model.File.ContentType, model.File.OpenReadStream());

            return user.ImageUrl;
        }

        [HttpDelete("change/image")]
        public async Task DeleteImage()
        {
            var userId = User.GetUserId();

            await _userService.DeleteImageAsync(userId);
        }

        [HttpPut("change/password")]
        public async Task<UserDetailsViewModel> ChangePassword(ChangePasswordViewModel model)
        {
            var userId = User.GetUserId();
            var user = await _userService.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

            return _mapper.Map<UserDetailsViewModel>(user);
        }

        [HttpPut("change/email")]
        public async Task ChangeEmail(ChangeEmailViewModel model)
        {
            var userId = User.GetUserId();
            var token = await _userService.ChangeEmailTokenAsync(userId, model.Email);
            var parameters = HttpUtility.ParseQueryString(string.Empty);

            parameters.Add(nameof(ConfirmChangeEmailViewModel.Email), model.Email);
            parameters.Add(nameof(ConfirmChangeEmailViewModel.Token), token);

            var urlBuilder = new UriBuilder(_clientSettings.BaseUrl)
            {
                Path = _clientSettings.EmailChangeConfirmPath,
                Query = parameters.ToString()
            };

            await _emailService.SendEmailChangeConfirmationAsync(model.Email, urlBuilder.Uri.ToString());
        }

        [HttpPut("change/phone")]
        public async Task ChangePhone(ChangePhoneNumberViewModel model)
        {
            var userId = User.GetUserId();
            var token = await _userService.ChangePhoneNumberTokenAsync(userId, model.PhoneNumber);

            await _smsService.SendConfirmationPhoneAsync(model.PhoneNumber, token);
        }

        [HttpPost("confirm/changeEmail")]
        public async Task ConfirmChangeEmail(ConfirmChangeEmailViewModel model)
        {
            await _userService.ChangeEmailAsync(User.GetUserId(), model.Email, model.Token);
        }

        [HttpPost("confirm/phone")]
        public async Task<UserDetailsViewModel> ConfirmChangePhone(ConfirmChangePhoneNumberViewModel model)
        {
            var userId = User.GetUserId();
            var user = await _userService.ChangePhoneNumberAsync(userId, model.Phone, model.Token);

            return _mapper.Map<UserDetailsViewModel>(user);
        }
    }
}