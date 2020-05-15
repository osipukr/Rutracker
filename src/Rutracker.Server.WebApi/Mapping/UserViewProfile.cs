using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Mapping
{
    public class UserViewProfile : Profile
    {
        public UserViewProfile()
        {
            CreateMap<User, UserView>().ReverseMap();
            CreateMap<User, UserDetailView>().ReverseMap();
            CreateMap<User, UserUpdateView>().ReverseMap();
        }
    }
}