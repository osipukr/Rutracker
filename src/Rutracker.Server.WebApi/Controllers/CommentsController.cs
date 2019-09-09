using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Comment;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize]
    public class CommentsController : BaseApiController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService, IMapper mapper) : base(mapper)
        {
            _commentService = commentService;
        }

        [HttpGet, AllowAnonymous]
        public async Task<PaginationResult<CommentViewModel>> List(int page, int pageSize, int torrentId)
        {
            var (comments, count) = await _commentService.ListAsync(page, pageSize, torrentId);

            return new PaginationResult<CommentViewModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = count,
                Items = _mapper.Map<IEnumerable<CommentViewModel>>(comments)
            };
        }

        [HttpGet("{id}")]
        public async Task<CommentViewModel> Find(int id)
        {
            var comment = await _commentService.FindAsync(id, User.GetUserId());

            return _mapper.Map<CommentViewModel>(comment);
        }

        [HttpPost]
        public async Task<CommentViewModel> Add(CommentCreateViewModel model)
        {
            var comment = _mapper.Map<Comment>(model);

            comment.UserId = User.GetUserId();

            var result = await _commentService.AddAsync(comment);

            return _mapper.Map<CommentViewModel>(result);
        }

        [HttpPut("{id}")]
        public async Task<CommentViewModel> Update(int id, CommentUpdateViewModel model)
        {
            var comment = _mapper.Map<Comment>(model);
            var result = await _commentService.UpdateAsync(id, User.GetUserId(), comment);

            return _mapper.Map<CommentViewModel>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _commentService.DeleteAsync(id, User.GetUserId());
        }

        [HttpGet("like/{id}")]
        public async Task<CommentViewModel> Like(int id)
        {
            var comment = await _commentService.LikeCommentAsync(id, User.GetUserId());

            return _mapper.Map<CommentViewModel>(comment);
        }
    }
}