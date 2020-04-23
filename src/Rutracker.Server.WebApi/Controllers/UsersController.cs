using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rutracker.Server.BusinessLayer.Collections;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Server.WebApi.Options;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Role;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The User API controller.
    /// </summary>
    [Authorize(Policy = Policies.IsUser)]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        private readonly ClientOptions _clientOptions;

        public UsersController(
            IUserService userService,
            IEmailService emailService,
            IMapper mapper,
            IOptions<ClientOptions> clientOptions) : base(mapper)
        {
            _userService = userService;
            _emailService = emailService;
            _clientOptions = clientOptions.Value;
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

        [HttpGet("profile")]
        public async Task<UserDetailView> Get()
        {
            var userId = User.GetUserId();
            var user = await _userService.FindAsync(userId);
            var roles = await _userService.RolesAsync(userId);

            var result = _mapper.Map<UserDetailView>(user);

            result.Roles = _mapper.Map<IEnumerable<RoleView>>(roles);

            return result;
        }

        [HttpPut("profile")]
        public async Task<UserDetailView> ChangeInfo(UserUpdateView model)
        {
            var userId = User.GetUserId();
            var user = _mapper.Map<User>(model);
            var result = await _userService.UpdateAsync(userId, user);

            return _mapper.Map<UserDetailView>(result);
        }

        [HttpPost("change/password")]
        public async Task<UserDetailView> ChangePassword(PasswordUpdateView model)
        {
            var userId = User.GetUserId();
            var user = await _userService.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

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

            var urlBuilder = new UriBuilder(_clientOptions.BaseUrl)
            {
                Path = _clientOptions.ResetPasswordPath,
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