using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Category;

namespace Rutracker.Server.WebApi.Mapping
{
    public class CategoryViewProfile : Profile
    {
        public CategoryViewProfile()
        {
            CreateMap<Category, CategoryView>()
                .ForMember(x => x.SubcategoriesCount,
                    x => x.MapFrom(y => y.Subcategories.Count))
                .ReverseMap();

            CreateMap<Category, CategoryDetailView>().ReverseMap();
            CreateMap<Category, CategoryCreateView>().ReverseMap();
            CreateMap<Category, CategoryUpdateView>().ReverseMap();
        }
    }
}