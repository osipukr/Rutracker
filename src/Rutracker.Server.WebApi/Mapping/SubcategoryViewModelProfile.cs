using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Server.WebApi.Mapping
{
    public class SubcategoryViewModelProfile : Profile
    {
        public SubcategoryViewModelProfile()
        {
            CreateMap<Subcategory, SubcategoryViewModel>()
                .ForMember(x => x.TorrentsCount,
                    x => x.MapFrom(y => y.Torrents.Count));
        }
    }
}