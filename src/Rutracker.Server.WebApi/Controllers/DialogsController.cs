using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Dialog;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize(Policy = Policies.IsUser)]
    public class DialogsController : BaseApiController
    {
        private readonly IDialogService _dialogService;

        public DialogsController(IDialogService dialogService, IMapper mapper) : base(mapper)
        {
            _dialogService = dialogService;
        }

        [HttpGet]
        public async Task<IEnumerable<DialogViewModel>> List()
        {
            var dialogs = await _dialogService.ListAsync(User.GetUserId());

            return _mapper.Map<IEnumerable<DialogViewModel>>(dialogs);
        }

        [HttpGet("{id}")]
        public async Task<DialogViewModel> Find(int id)
        {
            var dialog = await _dialogService.FindAsync(id, User.GetUserId());

            return _mapper.Map<DialogViewModel>(dialog);
        }

        //[HttpDelete("{id}")]
        //public async Task Delete(int id)
        //{
        //    await _dialogService.DeleteAsync(id, User.GetUserId());
        //}
    }
}