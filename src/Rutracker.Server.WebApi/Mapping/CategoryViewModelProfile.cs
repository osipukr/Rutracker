using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Category;

namespace Rutracker.Server.WebApi.Mapping
{
    public class CategoryViewModelProfile : Profile
    {
        public CategoryViewModelProfile()
        {
            CreateMap<Category, CategoryViewModel>()
                .ForMember(x => x.SubcategoriesCount,
                    x => x.MapFrom(y => y.Subcategories.Count));

            CreateMap<CategoryCreateViewModel, Category>();
            CreateMap<CategoryUpdateViewModel, Category>();
        }
    }
}