using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Torrent;

namespace Rutracker.Server.WebApi.Mapping
{
    public class TorrentViewModelProfile : Profile
    {
        public TorrentViewModelProfile()
        {
            CreateMap<Torrent, TorrentViewModel>()
                .ForMember(x => x.CommentsCount,
                    x => x.MapFrom(y => y.Comments.Count));

            CreateMap<Torrent, TorrentDetailsViewModel>();
        }
    }
}