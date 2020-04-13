using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Mapping
{
    public class TorrentViewProfile : Profile
    {
        public TorrentViewProfile()
        {
            CreateMap<Torrent, TorrentView>()
                .ForMember(x => x.CommentsCount,
                    x => x.MapFrom(y => y.Comments.Count))
                .ReverseMap();

            CreateMap<Torrent, TorrentDetailsView>().ReverseMap();
            CreateMap<Torrent, TorrentCreateView>().ReverseMap();
            CreateMap<Torrent, TorrentUpdateView>().ReverseMap();
        }
    }
}