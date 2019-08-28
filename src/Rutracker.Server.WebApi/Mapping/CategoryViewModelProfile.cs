using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Mapping
{
    public class CategoryViewModelProfile : Profile
    {
        public CategoryViewModelProfile() => CreateMap<Category, CategoryViewModel>();
    }
}