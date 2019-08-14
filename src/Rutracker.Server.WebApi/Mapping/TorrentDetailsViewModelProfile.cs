using AutoMapper;
using CodeKicker.BBCode;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Mapping
{
    public class TorrentDetailsViewModelProfile : Profile
    {
        public TorrentDetailsViewModelProfile() =>
            CreateMap<Torrent, TorrentDetailsItemViewModel>()
                .ForMember(dist => dist.Content,
                    opt => opt.MapFrom(src => BBCode.ToHtml(src.Content)));
    }
}