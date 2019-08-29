using System;
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
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LikeService(ILikeRepository likeRepository, ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _likeRepository = likeRepository;
            _commentRepository = commentRepository;
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

        public async Task<Like> FindAsync(int commentId, string userId)
        {
            if (commentId < 1)
            {
                throw new RutrackerException($"The {nameof(commentId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new RutrackerException($"The {nameof(userId)} is null or white space.", ExceptionEventType.NotValidParameters);
            }

            var like = await _likeRepository.GetAsync(x => x.CommentId == commentId && x.UserId == userId);

            if (like == null)
            {
                throw new RutrackerException("Like not found.", ExceptionEventType.NotFound);
            }

            return like;
        }

        public async Task<bool> IsUserLiked(int commentId, string userId)
        {
            if (commentId < 1)
            {
                throw new RutrackerException($"The {nameof(commentId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new RutrackerException($"The {nameof(userId)} is null or white space.", ExceptionEventType.NotValidParameters);
            }

            return await _likeRepository.ExistAsync(x => x.CommentId == commentId && x.UserId == userId);
        }

        public async Task AddAsync(Like like)
        {
            if (like == null)
            {
                throw new RutrackerException("Not valid like.", ExceptionEventType.NotValidParameters);
            }

            if (!await _commentRepository.ExistAsync(like.CommentId))
            {
                throw new RutrackerException($"Error adding like. Not found comment with id {like.CommentId}.", ExceptionEventType.NotValidParameters);
            }

            await _likeRepository.AddAsync(like);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int commentId, string userId)
        {
            var like = await FindAsync(commentId, userId);

            _likeRepository.Remove(like);

            await _unitOfWork.CompleteAsync();
        }
    }
}