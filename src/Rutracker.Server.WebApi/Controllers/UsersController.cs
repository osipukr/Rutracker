using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Collections;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
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

        public UsersController(IUserService userService, IMapper mapper) : base(mapper)
        {
            _userService = userService;
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

            var user = await _userService.FindByIdAsync(userId);
            var roles = await _userService.RolesAsync(userId);

            var result = _mapper.Map<UserDetailView>(user);

            result.Roles = _mapper.Map<IEnumerable<RoleView>>(roles);

            return result;
        }

        [HttpPut("profile")]
        public async Task<UserDetailView> Put(UserUpdateView model)
        {
            var userId = User.GetUserId();
            var user = _mapper.Map<User>(model);

            var newUser = await _userService.UpdateAsync(userId, user);
            var roles = await _userService.RolesAsync(userId);

            var result = _mapper.Map<UserDetailView>(newUser);

            result.Roles = _mapper.Map<IEnumerable<RoleView>>(roles);

            return result;
        }

        [HttpPost("profile/password")]
        public async Task<UserDetailView> Post(PasswordUpdateView model)
        {
            var userId = User.GetUserId();

            var user = await _userService.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            var roles = await _userService.RolesAsync(userId);

            var result = _mapper.Map<UserDetailView>(user);

            result.Roles = _mapper.Map<IEnumerable<RoleView>>(roles);

            return result;
        }
    }
}