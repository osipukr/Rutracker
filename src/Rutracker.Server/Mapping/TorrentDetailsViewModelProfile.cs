using AutoMapper;
using Rutracker.Core.Entities;
using Rutracker.Server.Helpers;
using Rutracker.Shared.ViewModels.Torrent;

namespace Rutracker.Server.Mapping
{
    public class TorrentDetailsViewModelProfile : Profile
    {
        public TorrentDetailsViewModelProfile() =>
            CreateMap<Torrent, TorrentDetailsItemViewModel>()
                .ForMember(dist => dist.Content,
                    opt => opt.MapFrom(src => BBCodeHelper.ParseToHtml(src.Content)));
    }
}