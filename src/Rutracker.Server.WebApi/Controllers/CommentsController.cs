using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Shared.Models.ViewModels.Torrent;
using Rutracker.Shared.Models.ViewModels.Torrent.Create;
using Rutracker.Shared.Models.ViewModels.Torrent.Update;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize]
    public class CommentsController : BaseApiController
    {
        private readonly ICommentService _commentService;
        private readonly ILikeService _likeService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, ILikeService likeService, IMapper mapper)
        {
            _commentService = commentService;
            _likeService = likeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentViewModel>> List(int torrentId, int count)
        {
            var comments = await _commentService.ListAsync(torrentId, count);

            return _mapper.Map<IEnumerable<CommentViewModel>>(comments);
        }

        [HttpPost(nameof(Add))]
        public async Task Add(CommentCreateViewModel model)
        {
            var comment = _mapper.Map<Comment>(model);

            comment.UserId = User.GetUserId();

            await _commentService.AddAsync(comment);
        }

        [HttpPut(nameof(Update))]
        public async Task Update(int id, CommentUpdateViewModel model)
        {
            var comment = _mapper.Map<Comment>(model);

            await _commentService.UpdateAsync(id, User.GetUserId(), comment);
        }

        [HttpDelete(nameof(Delete))]
        public async Task Delete(int id)
        {
            await _commentService.DeleteAsync(id, User.GetUserId());
        }

        [HttpGet(nameof(Like))]
        public async Task Like(int id)
        {
            await _likeService.LikeCommentAsync(id, User.GetUserId());
        }
    }
}