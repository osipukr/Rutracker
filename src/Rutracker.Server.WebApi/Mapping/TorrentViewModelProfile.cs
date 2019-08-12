using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Torrents;

namespace Rutracker.Server.WebApi.Mapping
{
    public class TorrentViewModelProfile : Profile
    {
        public TorrentViewModelProfile() => CreateMap<Torrent, TorrentItemViewModel>();
    }
}