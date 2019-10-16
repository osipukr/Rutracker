using System.Linq;
using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Dialog;

namespace Rutracker.Server.WebApi.Mapping
{
    public class DialogViewModelProfile : Profile
    {
        public DialogViewModelProfile()
        {
            CreateMap<Dialog, DialogViewModel>()
                .ForMember(dialogView => dialogView.Users,
                    config => config.MapFrom(dialog => dialog.UserDialogs.Select(x => x.User)))
                .ForMember(dialogView => dialogView.UsersCount,
                    config => config.MapFrom(dialog => dialog.UserDialogs.Count));

            CreateMap<DialogCreateViewModel, Dialog>();
        }
    }
}