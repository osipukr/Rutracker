using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Server.WebApi.Mapping
{
    public class FileViewModelProfile : Profile
    {
        public FileViewModelProfile()
        {
            CreateMap<File, FileViewModel>();
            CreateMap<FileCreateViewModel, File>();
            CreateMap<FileUpdateViewModel, File>();
        }
    }
}