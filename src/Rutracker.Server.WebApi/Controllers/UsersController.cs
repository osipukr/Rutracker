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
using Rutracker.Shared.Infrastructure.Collections;
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
    public class UsersController : ApiController
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IPagedList<UserView>> Get([FromQuery] UserFilter filter)
        {
            var pagedList = await _userService.ListAsync(filter);

            return PagedList.From(pagedList, users => _mapper.Map<IEnumerable<UserView>>(users));
        }

        [HttpGet("{userName}")]
        [AllowAnonymous]
        public async Task<UserView> Get(string userName)
        {
            var user = await _userService.FindByNameAsync(userName);

            return _mapper.Map<UserView>(user);
        }

        [HttpGet("details")]
        public async Task<UserDetailView> Get()
        {
            var userId = User.GetUserId();
            var user = await _userService.FindAsync(userId);

            var result = _mapper.Map<UserDetailView>(user);

            result.Roles = await _userService.RolesAsync(userId);

            return result;
        }

        [HttpPut("change/info")]
        public async Task<UserDetailView> ChangeInfo(UserUpdateView model)
        {
            var userId = User.GetUserId();
            var user = _mapper.Map<User>(model);
            var result = await _userService.UpdateAsync(userId, user);

            return _mapper.Map<UserDetailView>(result);
        }

        [HttpPut("change/image")]
        public async Task<string> ChangeImage(ImageUpdateView model)
        {
            var userId = User.GetUserId();
            var user = await _userService.ChangeImageAsync(userId, model.ImageUrl);

            return user.ImageUrl;
        }

        [HttpPost("change/image")]
        public async Task<string> ChangeImage([FromForm] ImageFileUpdateView model)
        {
            var userId = User.GetUserId();
            var user = await _userService.ChangeImageAsync(userId, model.File);

            return user.ImageUrl;
        }

        [HttpDelete("change/image")]
        public async Task DeleteImage()
        {
            var userId = User.GetUserId();

            await _userService.DeleteImageAsync(userId);
        }

        [HttpPut("change/password")]
        public async Task<UserDetailView> ChangePassword(PasswordUpdateView model)
        {
            var userId = User.GetUserId();
            var user = await _userService.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

            return _mapper.Map<UserDetailView>(user);
        }

        //[HttpPut("change/email")]
        //public async Task ChangeEmail(ChangeEmailViewModel model)
        //{
        //    var userId = User.GetUserId();
        //    var token = await _userService.ChangeEmailTokenAsync(userId, model.Email);
        //    var parameters = HttpUtility.ParseQueryString(string.Empty);

        //    parameters.Add(nameof(ConfirmChangeEmailViewModel.Email), model.Email);
        //    parameters.Add(nameof(ConfirmChangeEmailViewModel.Token), token);

        //    var urlBuilder = new UriBuilder(_clientSettings.BaseUrl)
        //    {
        //        Path = _clientSettings.EmailChangeConfirmPath,
        //        Query = parameters.ToString()
        //    };

        //    await _emailService.SendEmailChangeConfirmationAsync(model.Email, urlBuilder.Uri.ToString());
        //}

        [HttpPut("change/phone")]
        public async Task ChangePhone(PhoneUpdateView model)
        {
            var userId = User.GetUserId();
            var token = await _userService.ChangePhoneNumberTokenAsync(userId, model.PhoneNumber);

            await _smsService.SendPhoneConfirmationAsync(model.PhoneNumber, token);
        }

        [HttpPost("confirm/changeEmail")]
        public async Task ConfirmChangeEmail(EmailConfirmationView model)
        {
            await _userService.ChangeEmailAsync(User.GetUserId(), model.Email, model.Token);
        }

        [HttpPost("confirm/phone")]
        public async Task<UserDetailView> ConfirmChangePhone(PhoneConfirmationView model)
        {
            var userId = User.GetUserId();
            var user = await _userService.ChangePhoneNumberAsync(userId, model.Phone, model.Token);

            return _mapper.Map<UserDetailView>(user);
        }

        [HttpPost("forgotPassword")]
        public async Task ForgotPassword(ForgotPasswordView model)
        {
            var user = await _userService.FindByNameAsync(model.UserName);
            var token = await _userService.PasswordResetTokenAsync(user.Id);
            var parameters = HttpUtility.ParseQueryString(string.Empty);

            parameters.Add(nameof(ResetPasswordView.UserId), user.Id);
            parameters.Add(nameof(ResetPasswordView.Token), token);

            var urlBuilder = new UriBuilder(_clientSettings.BaseUrl)
            {
                Path = _clientSettings.ResetPasswordPath,
                Query = parameters.ToString()
            };

            await _emailService.SendResetPasswordAsync(user.Email, urlBuilder.Uri.ToString());
        }

        [HttpPost("resetPassword")]
        public async Task ResetPassword(ResetPasswordView model)
        {
            await _userService.ResetPasswordAsync(model.UserId, model.Password, model.Token);
        }
    }
}