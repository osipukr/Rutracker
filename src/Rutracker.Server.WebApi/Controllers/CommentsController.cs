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
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Models;
using Rutracker.Shared.Models.ViewModels.Comment;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize(Policy = Policies.IsUser)]
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService, IMapper mapper) : base(mapper)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IPagedList<CommentView>> Get([FromQuery] CommentFilter filter)
        {
            var pagedList = await _commentService.ListAsync(filter);

            return PagedList.From(pagedList, comments => _mapper.Map<IEnumerable<CommentView>>(comments));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<CommentView> Get(int id)
        {
            var comment = await _commentService.FindAsync(id);

            return _mapper.Map<CommentView>(comment);
        }

        [HttpPost]
        public async Task<CommentView> Post(CommentCreateView model)
        {
            var comment = _mapper.Map<Comment>(model);

            comment.UserId = User.GetUserId();

            var result = await _commentService.AddAsync(comment);

            return _mapper.Map<CommentView>(result);
        }

        [HttpPut("{id}")]
        public async Task<CommentView> Put(int id, CommentUpdateView model)
        {
            var comment = _mapper.Map<Comment>(model);
            var result = await _commentService.UpdateAsync(id, User.GetUserId(), comment);

            return _mapper.Map<CommentView>(result);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _commentService.DeleteAsync(id, User.GetUserId());
        }

        [HttpGet("like/{id}")]
        public async Task<CommentView> Like(int id)
        {
            var comment = await _commentService.LikeCommentAsync(id, User.GetUserId());

            return _mapper.Map<CommentView>(comment);
        }
    }
}