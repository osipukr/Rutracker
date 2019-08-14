using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Users;

namespace Rutracker.Server.WebApi.Mapping
{
    public class UserViewModelProfile : Profile
    {
        public UserViewModelProfile() => CreateMap<User, UserViewModel>();
    }
}