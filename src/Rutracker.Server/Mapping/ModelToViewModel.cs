using AutoMapper;
using Rutracker.Core.Entities;
using Rutracker.Server.Helpers;
using Rutracker.Shared.ViewModels.Torrent;
using Rutracker.Shared.ViewModels.Torrents;

namespace Rutracker.Server.Mapping
{
    public class ModelToViewModel : Profile
    {
        public ModelToViewModel()
        {
            CreateMap<Torrent, TorrentItemViewModel>();
            CreateMap<Torrent, TorrentDetailsItemViewModel>().ForMember(
                dist => dist.Content,
                opt => opt.MapFrom(
                src => BBCodeHelper.ParseToHtml(src.Content)));
            CreateMap<File, FileItemViewModel>();
        }
    }
}