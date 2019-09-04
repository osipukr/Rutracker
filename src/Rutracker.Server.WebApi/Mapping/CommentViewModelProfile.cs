using System.Linq;
using AutoMapper;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Shared.Models.ViewModels.Comment;

namespace Rutracker.Server.WebApi.Mapping
{
    public class CommentViewModelProfile : Profile
    {
        public CommentViewModelProfile()
        {
            CreateMap<Comment, CommentViewModel>().ForMember(x => x.LikesCount, x => x.MapFrom(y => y.Likes.Count));
            CreateMap<CommentCreateViewModel, Comment>();
            CreateMap<CommentUpdateViewModel, Comment>();
        }
    }
}