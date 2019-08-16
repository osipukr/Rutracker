using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.User;

namespace Rutracker.Server.WebApi.Mapping
{
    public class UserDetailsViewModelProfile : Profile
    {
        public UserDetailsViewModelProfile() => CreateMap<User, UserDetailsViewModel>();
    }
}