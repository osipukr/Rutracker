using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Message;

namespace Rutracker.Server.WebApi.Mapping
{
    public class MessageViewModelProfile : Profile
    {
        public MessageViewModelProfile()
        {
            CreateMap<Message, MessageViewModel>();
            CreateMap<MessageCreateViewModel, Message>();
        }
    }
}