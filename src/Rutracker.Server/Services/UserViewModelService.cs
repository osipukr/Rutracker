using System;
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

        public async Task<UserViewModel[]> GetUsersAsync()
        {
            var users = await _userService.GetAllUserAsync();

            return _mapper.Map<UserViewModel[]>(users);
        }

        public async Task<UserViewModel> GetUserAsync(ClaimsPrincipal principal)
        {
            var user = await _userService.GetUserAsync(principal);

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task UpdateUserAsync(ClaimsPrincipal principal, EditUserViewModel model)
        {
            var user = await _userService.GetUserAsync(principal);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.ImageUrl = model.ImageUrl;

            await _userService.UpdateUserAsync(user);
        }
    }
}