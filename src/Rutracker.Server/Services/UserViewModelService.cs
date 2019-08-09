using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Rutracker.Core.Interfaces.Services;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Server.Services
{
    public class UserViewModelService : IUserViewModelService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserViewModelService(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IReadOnlyList<UserViewModel>> GetUsersAsync()
        {
            var users = await _userService.GetAllUserAsync();

            return _mapper.Map<UserViewModel[]>(users);
        }

        public async Task<UserResponseViewModel> GetUserAsync(ClaimsPrincipal principal)
        {
            var user = await _userService.GetUserAsync(principal);
            var roles = await _userService.GetUserRolesAsync(user);

            return new UserResponseViewModel
            {
                User = _mapper.Map<UserViewModel>(user),
                Roles = _mapper.Map<RoleViewModel[]>(roles)
            };
        }
    }
}