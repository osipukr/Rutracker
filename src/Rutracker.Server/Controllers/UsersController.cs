using System;
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
        public async Task<UserViewModel[]> GetAll() => await _userViewModelService.GetUsersAsync();

        [HttpGet(nameof(Details))]
        public async Task<UserViewModel> Details() => await _userViewModelService.GetUserAsync(User);
    }
}