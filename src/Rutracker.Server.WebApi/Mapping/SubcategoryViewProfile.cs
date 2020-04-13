using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Subcategory;

namespace Rutracker.Server.WebApi.Mapping
{
    public class SubcategoryViewProfile : Profile
    {
        public SubcategoryViewProfile()
        {
            CreateMap<Subcategory, SubcategoryView>()
                .ForMember(x => x.TorrentsCount,
                    x => x.MapFrom(y => y.Torrents.Count))
                .ReverseMap();

            CreateMap<Subcategory, SubcategoryDetailView>().ReverseMap();
            CreateMap<Subcategory, SubcategoryCreateView>().ReverseMap();
            CreateMap<Subcategory, SubcategoryUpdateView>().ReverseMap();
        }
    }
}