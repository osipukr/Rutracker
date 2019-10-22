using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Server.WebApi.Interfaces;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Dialog;
using Rutracker.Shared.Models.ViewModels.Message;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Hubs
{
    [Authorize(Policy = Policies.IsUser)]
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IDialogService _dialogService;
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ChatHub(
            IDialogService dialogService,
            IMessageService messageService,
            IUserService userService,
            IMapper mapper)
        {
            _dialogService = dialogService;
            _messageService = messageService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task CreateDialogAsync(DialogCreateViewModel dialogCreateView)
        {
            var dialog = _mapper.Map<Dialog>(dialogCreateView);

            dialog.UserId = Context.User.GetUserId();
            dialog = await _dialogService.AddAsync(dialog, dialogCreateView.UserIds);

            var result = _mapper.Map<DialogViewModel>(dialog);

            await Clients.All.ReceiveDialogAsync(result);
        }

        public async Task JoinDialogAsync(int dialogId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, dialogId.ToString());

            var user = await _userService.FindAsync(Context.User.GetUserId());
            var result = _mapper.Map<UserViewModel>(user);

            await Clients.Caller.JoinDialogAsync(result);
        }

        public async Task LeaveDialogAsync(int dialogId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, dialogId.ToString());

            var user = await _userService.FindAsync(Context.User.GetUserId());
            var result = _mapper.Map<UserViewModel>(user);

            await Clients.Caller.LeaveDialogAsync(result);
        }

        public async Task SendMessageAsync(MessageCreateViewModel messageCreateView)
        {
            var message = _mapper.Map<Message>(messageCreateView);

            message.UserId = Context.User.GetUserId();
            message = await _messageService.AddAsync(message);

            var result = _mapper.Map<MessageViewModel>(message);

            await Clients.Group(message.DialogId.ToString()).ReceiveMessageAsync(result);
        }
    }
}