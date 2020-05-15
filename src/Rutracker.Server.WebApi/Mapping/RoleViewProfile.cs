using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Role;

namespace Rutracker.Server.WebApi.Mapping
{
    public class RoleViewProfile : Profile
    {
        public RoleViewProfile()
        {
            CreateMap<Role, RoleView>().ReverseMap();
        }
    }
}