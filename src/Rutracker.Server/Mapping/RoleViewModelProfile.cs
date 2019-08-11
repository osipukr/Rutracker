using AutoMapper;
using Rutracker.Core.Entities.Identity;
using Rutracker.Shared.ViewModels.Users;

namespace Rutracker.Server.Mapping
{
    public class RoleViewModelProfile : Profile
    {
        public RoleViewModelProfile() => CreateMap<Role, RoleViewModel>();
    }
}