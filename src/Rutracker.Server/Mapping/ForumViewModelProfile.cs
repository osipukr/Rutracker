using AutoMapper;
using Rutracker.Core.Entities;
using Rutracker.Shared.ViewModels.Torrent;

namespace Rutracker.Server.Mapping
{
    public class ForumViewModelProfile : Profile
    {
        public ForumViewModelProfile() => CreateMap<Forum, ForumItemViewModel>();
    }
}