using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.WebApi.Controllers.Base;
using Rutracker.Server.WebApi.Extensions;
using Rutracker.Shared.Models.ViewModels.Torrent.Create;

namespace Rutracker.Server.WebApi.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly ILikeService _likeService;
        private readonly IMapper _mapper;

        public LikesController(ILikeService likeService, IMapper mapper)
        {
            _likeService = likeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task Like(LikeCreateViewModel model)
        {
            var userId = User.GetUserId();

            if (await _likeService.IsUserLiked(model.CommentId, userId))
            {
                await _likeService.DeleteAsync(model.CommentId, userId);
            }
            else
            {
                await _likeService.AddAsync(_mapper.Map<Like>(model));
            }
        }
    }
}