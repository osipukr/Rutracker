using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Message;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize(Policy = Policies.IsUser)]
    public class MessagesController : BaseApiController
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService, IMapper mapper) : base(mapper)
        {
            _messageService = messageService;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<MessageViewModel>> Search(int dialogId)
        {
            var messages = await _messageService.ListAsync(dialogId, User.GetUserId());

            return _mapper.Map<IEnumerable<MessageViewModel>>(messages);
        }

        [HttpGet("{id}")]
        public async Task<MessageViewModel> Find(int id)
        {
            var dialog = await _messageService.FindAsync(id, User.GetUserId());

            return _mapper.Map<MessageViewModel>(dialog);
        }

        [HttpPost]
        public async Task<MessageViewModel> Create(MessageCreateViewModel model)
        {
            var message = _mapper.Map<Message>(model);

            message.UserId = User.GetUserId();

            var result = await _messageService.AddAsync(message);

            return _mapper.Map<MessageViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _messageService.DeleteAsync(id, User.GetUserId());
        }
    }
}