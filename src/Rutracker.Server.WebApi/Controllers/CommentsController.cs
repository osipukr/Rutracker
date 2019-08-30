using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Shared.Models.ViewModels.Torrent.Create;
using Rutracker.Shared.Models.ViewModels.Torrent.Update;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize]
    public class CommentsController : BaseApiController
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost(nameof(Add))]
        public async Task Add(CommentCreateViewModel model)
        {
            var comment = _mapper.Map<Comment>(model);

            comment.UserId = User.GetUserId();

            await _commentService.AddAsync(comment);
        }

        [HttpPut(nameof(Update))]
        public async Task Update(CommentUpdateViewModel model)
        {
            var comment = _mapper.Map<Comment>(model);

            await _commentService.UpdateAsync(comment.Id, User.GetUserId(), comment);
        }

        [HttpPost(nameof(Like))]
        public async Task Like(LikeCreateViewModel model)
        {
            await _commentService.LikeCommentAsync(model.CommentId, User.GetUserId());
        }
    }
}