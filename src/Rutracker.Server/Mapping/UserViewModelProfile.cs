using AutoMapper;
using Rutracker.Core.Entities.Identity;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Server.Mapping
{
    public class UserViewModelProfile : Profile
    {
        public UserViewModelProfile() => CreateMap<User, UserViewModel>();
    }
}