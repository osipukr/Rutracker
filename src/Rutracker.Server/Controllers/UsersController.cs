using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.Interfaces;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Server.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserViewModelService _userViewModelService;

        public UsersController(IUserViewModelService userViewModelService)
        {
            _userViewModelService = userViewModelService ?? throw new ArgumentNullException(nameof(userViewModelService));
        }

        [HttpGet]
        public async Task<IReadOnlyList<UserViewModel>> GetAll() => await _userViewModelService.GetUsersAsync();

        [HttpGet(nameof(Details))]
        public async Task<UserResponseViewModel> Details() => await _userViewModelService.GetUserAsync(User);
    }
}