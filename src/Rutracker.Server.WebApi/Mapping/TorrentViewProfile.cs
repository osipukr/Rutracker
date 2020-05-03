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
                .ForMember(x => x.Category,
                    x => x.MapFrom(y => y.Subcategory.Category))
                .ReverseMap();

            CreateMap<Torrent, TorrentCreateView>().ReverseMap();
            CreateMap<Torrent, TorrentUpdateView>().ReverseMap();
        }
    }
}