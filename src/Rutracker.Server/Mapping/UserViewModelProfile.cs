using AutoMapper;
using Rutracker.Core.Entities.Accounts;
using Rutracker.Shared.ViewModels.Accounts;

namespace Rutracker.Server.Mapping
{
    public class UserViewModelProfile : Profile
    {
        public UserViewModelProfile() => CreateMap<User, UserViewModel>();
    }
}