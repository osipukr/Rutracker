using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Comment;

namespace Rutracker.Server.WebApi.Mapping
{
    public class CommentViewProfile : Profile
    {
        public CommentViewProfile()
        {
            CreateMap<Comment, CommentView>()
                .ForMember(x => x.LikesCount,
                    x => x.MapFrom(y => y.Likes.Count))
                .ReverseMap();

            CreateMap<Comment, CommentCreateView>().ReverseMap();
            CreateMap<Comment, CommentUpdateView>().ReverseMap();
        }
    }
}