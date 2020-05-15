using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.File;

namespace Rutracker.Server.WebApi.Mapping
{
    public class FileViewProfile : Profile
    {
        public FileViewProfile()
        {
            CreateMap<File, FileView>().ReverseMap();
        }
    }
}