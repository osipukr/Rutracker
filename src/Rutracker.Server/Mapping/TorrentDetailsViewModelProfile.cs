using AutoMapper;
using CodeKicker.BBCode;
using Rutracker.Core.Entities.Torrents;
using Rutracker.Shared.ViewModels.Torrent;

namespace Rutracker.Server.Mapping
{
    public class TorrentDetailsViewModelProfile : Profile
    {
        public TorrentDetailsViewModelProfile() =>
            CreateMap<Torrent, TorrentDetailsItemViewModel>()
                .ForMember(dist => dist.Content,
                    opt => opt.MapFrom(src => BBCode.ToHtml(src.Content)));
    }
}