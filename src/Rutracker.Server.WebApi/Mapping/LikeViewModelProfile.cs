using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Like;

namespace Rutracker.Server.WebApi.Mapping
{
    public class LikeViewModelProfile : Profile
    {
        public LikeViewModelProfile()
        {
            CreateMap<Like, LikeViewModel>().ForMember(x => x.UserName, x => x.MapFrom(y => y.User.UserName));
        }
    }
}