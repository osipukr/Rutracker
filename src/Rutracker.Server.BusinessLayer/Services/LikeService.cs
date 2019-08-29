using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LikeService(ILikeRepository likeRepository, IUnitOfWork unitOfWork)
        {
            _likeRepository = likeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Like>> ListAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new RutrackerException($"The {nameof(userId)} is null or white space.", ExceptionEventType.NotValidParameters);
            }

            var likes = await _likeRepository.GetAll(x => x.UserId == userId).ToListAsync();

            if (likes == null)
            {
                throw new RutrackerException("Likes not found.", ExceptionEventType.NotFound);
            }

            return likes;
        }

        public async Task<IEnumerable<Like>> ListAsync(int commentId)
        {
            if (commentId < 1)
            {
                throw new RutrackerException($"The {nameof(commentId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            var likes = await _likeRepository.GetAll(x => x.CommentId == commentId).ToListAsync();

            if (likes == null)
            {
                throw new RutrackerException("Likes not found.", ExceptionEventType.NotFound);
            }

            return likes;
        }

        public async Task<Like> FindAsync(int likeId)
        {
            if (likeId < 1)
            {
                throw new RutrackerException($"The {nameof(likeId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            var like = await _likeRepository.GetAsync(likeId);

            if (like == null)
            {
                throw new RutrackerException("Like not found.", ExceptionEventType.NotFound);
            }

            return like;
        }

        public async Task<Like> FindAsync(int likeId, string userId)
        {
            if (likeId < 1)
            {
                throw new RutrackerException($"The {nameof(likeId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new RutrackerException($"The {nameof(userId)} is null or white space.", ExceptionEventType.NotValidParameters);
            }

            var like = await _likeRepository.GetAsync(x => x.Id == likeId && x.UserId == userId);

            if (like == null)
            {
                throw new RutrackerException("Like not found.", ExceptionEventType.NotFound);
            }

            return like;
        }

        public async Task AddAsync(Like like)
        {
            if (like == null)
            {
                throw new RutrackerException("Not valid like.", ExceptionEventType.NotValidParameters);
            }

            await _likeRepository.AddAsync(like);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int likeId, string userId)
        {
            var like = await FindAsync(likeId, userId);

            _likeRepository.Remove(like);

            await _unitOfWork.CompleteAsync();
        }
    }
}