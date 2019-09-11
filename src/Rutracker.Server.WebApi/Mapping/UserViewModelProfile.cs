using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Mapping
{
    public class UserViewModelProfile : Profile
    {
        public UserViewModelProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, UserProfileViewModel>();
            CreateMap<User, UserDetailsViewModel>();
            CreateMap<ChangeUserViewModel, User>();
        }
    }
}