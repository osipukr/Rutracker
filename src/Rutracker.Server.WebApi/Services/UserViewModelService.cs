using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Services
{
    public class UserViewModelService : IUserViewModelService
    {
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        public UserViewModelService(IUserService userService, IStorageService storageService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserViewModel[]> UsersAsync()
        {
            var users = await _userService.ListAsync();

            return _mapper.Map<UserViewModel[]>(users);
        }

        public async Task<UserDetailsViewModel> UserAsync(ClaimsPrincipal principal)
        {
            var userId = principal.GetUserId();
            var user = await _userService.FindAsync(userId);
            var roles = await _userService.RolesAsync(user);
            var userResult = _mapper.Map<UserDetailsViewModel>(user);

            userResult.Roles = roles.ToArray();

            return userResult;
        }

        public async Task UpdateAsync(ClaimsPrincipal principal, EditUserViewModel model)
        {
            var userId = principal.GetUserId();
            var user = await _userService.FindAsync(userId);

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            if (model.ImageBytes?.Length > 0)
            {
                await using var stream = new MemoryStream(model.ImageBytes);

                var containerName = user.UserName;
                var fileName = $"profile-image-{Guid.NewGuid()}";

                await _storageService.UploadFileAsync(containerName, fileName, stream);

                user.ImageUrl = await _storageService.PathToFileAsync(containerName, fileName);
            }
            else
            {
                user.ImageUrl = model.ImageUrl;
            }

            await _userService.UpdateAsync(user);
        }
    }
}