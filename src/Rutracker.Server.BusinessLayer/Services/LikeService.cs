using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LikeService(ILikeRepository likeRepository, ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _likeRepository = likeRepository;
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Like> LikeCommentAsync(int commentId, string userId)
        {
            Guard.Against.LessOne(commentId, $"The {nameof(commentId)} is less than 1.");
            Guard.Against.NullOrWhiteSpace(userId, message: $"The {nameof(userId)} is null or white space.");

            var like = await _likeRepository.GetAsync(x => x.CommentId == commentId && x.UserId == userId);

            if (like == null)
            {
                if (await _commentRepository.GetAsync(commentId) == null)
                {
                    throw new RutrackerException($"Not valid {commentId}.", ExceptionEventType.NotValidParameters);
                }

                like = new Like
                {
                    CommentId = commentId,
                    UserId = userId
                };

                await _likeRepository.AddAsync(like);
            }
            else
            {
                _likeRepository.Remove(like);
            }

            await _unitOfWork.CompleteAsync();

            return like;
        }
    }
}