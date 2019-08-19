using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Controllers
{
    /// <summary>
    ///     The User API controller.
    /// </summary>
    /// <response code="400">If the parameters are not valid.</response>
    /// <response code="401">If the user is not authorized.</response>
    /// <response code="404">If the item is null.</response>
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserViewModelService _userViewModelService;

        public UsersController(IUserViewModelService userViewModelService)
        {
            _userViewModelService = userViewModelService ?? throw new ArgumentNullException(nameof(userViewModelService));
        }

        [HttpGet]
        public async Task<UserViewModel[]> GetAll()
        {
            return await _userViewModelService.UsersAsync();
        }

        [HttpGet(nameof(Details))]
        public async Task<UserViewModel> Details()
        {
            return await _userViewModelService.UserAsync(User);
        }

        [HttpPut(nameof(ChangeUser))]
        public async Task ChangeUser(ChangeUserViewModel model)
        {
            await _userViewModelService.ChangeUserAsync(User, model);
        }

        [HttpPut(nameof(ChangeImage))]
        public async Task ChangeImage(ChangeImageViewModel model)
        {
            await _userViewModelService.ChangeImageAsync(User, model);
        }

        [HttpPut(nameof(ChangePassword))]
        public async Task ChangePassword(ChangePasswordViewModel model)
        {
            await _userViewModelService.ChangePasswordAsync(User, model);
        }

        [HttpPut(nameof(ChangeEmail))]
        public async Task ChangeEmail(ChangeEmailViewModel model)
        {
            await _userViewModelService.ChangeEmailAsync(User, model);
        }

        [HttpPut(nameof(ChangePhoneNumber))]
        public async Task ChangePhoneNumber(ChangePhoneNumberViewModel model)
        {
            await _userViewModelService.ChangePhoneNumberAsync(User, model);
        }

        [HttpDelete(nameof(DeleteImage))]
        public async Task DeleteImage()
        {
            await _userViewModelService.DeleteImageAsync(User);
        }

        [AllowAnonymous, HttpGet(nameof(ConfirmChangeEmail))]
        public async Task ConfirmChangeEmail([FromQuery] ConfirmChangeEmailViewModel model)
        {
            await _userViewModelService.ConfirmChangeEmailAsync(model);
        }

        [HttpPost(nameof(ConfirmChangePhoneNumber))]
        public async Task ConfirmChangePhoneNumber(ConfirmChangePhoneNumberViewModel model)
        {
            await _userViewModelService.ConfirmChangePhoneNumber(User, model);
        }
    }
}