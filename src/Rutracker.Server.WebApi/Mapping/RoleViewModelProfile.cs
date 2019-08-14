using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Mapping
{
    public class RoleViewModelProfile : Profile
    {
        public RoleViewModelProfile() => CreateMap<Role, RoleViewModel>();
    }
}