using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models.ViewModels.Users;

namespace Rutracker.Server.WebApi.Services
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
            var users = await _userService.ListAsync();

            return _mapper.Map<UserViewModel[]>(users);
        }

        public async Task<UserViewModel> GetUserAsync(ClaimsPrincipal principal)
        {
            var userId = principal.GetUserId();
            var user = await _userService.FindAsync(userId);

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task UpdateUserAsync(ClaimsPrincipal principal, EditUserViewModel model)
        {
            var userId = principal.GetUserId();
            var user = await _userService.FindAsync(userId);

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.ImageUrl = model.ImageUrl;

            await _userService.UpdateAsync(user);
        }
    }
}