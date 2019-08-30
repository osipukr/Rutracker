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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITorrentRepository _torrentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(
            ICommentRepository commentRepository,
            ITorrentRepository torrentRepository,
            ILikeRepository likeRepository,
            IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _torrentRepository = torrentRepository;
            _likeRepository = likeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Comment>> ListAsync(int torrentId)
        {
            if (torrentId < 1)
            {
                throw new RutrackerException($"The {nameof(torrentId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            var comments = await _commentRepository.GetAll(x => x.TorrentId == torrentId).ToListAsync();

            if (comments == null)
            {
                throw new RutrackerException("Comments not found.", ExceptionEventType.NotFound);
            }

            return comments;
        }

        public async Task<IEnumerable<Comment>> ListAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new RutrackerException($"The {nameof(userId)} is null or white space.", ExceptionEventType.NotValidParameters);
            }

            var comments = await _commentRepository.GetAll(x => x.UserId == userId).ToListAsync();

            if (comments == null)
            {
                throw new RutrackerException("Comments not found.", ExceptionEventType.NotFound);
            }

            return comments;
        }

        public async Task<Comment> FindAsync(int commentId)
        {
            if (commentId < 1)
            {
                throw new RutrackerException($"The {nameof(commentId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            var comment = await _commentRepository.GetAsync(commentId);

            if (comment == null)
            {
                throw new RutrackerException("Comment not found.", ExceptionEventType.NotFound);
            }

            return comment;
        }

        public async Task<Comment> FindAsync(int commentId, string userId)
        {
            if (commentId < 1)
            {
                throw new RutrackerException($"The {nameof(commentId)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new RutrackerException($"The {nameof(userId)} is null or white space.", ExceptionEventType.NotValidParameters);
            }

            var comment = await _commentRepository.GetAsync(x => x.Id == commentId && x.UserId == userId);

            if (comment == null)
            {
                throw new RutrackerException("Comment not found.", ExceptionEventType.NotFound);
            }

            return comment;
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            if (comment == null)
            {
                throw new RutrackerException("Not valid comment.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(comment.Text))
            {
                throw new RutrackerException("The comment must contain text.", ExceptionEventType.NotValidParameters);
            }

            if (comment.TorrentId < 1)
            {
                throw new RutrackerException("Invalid torrent ID for comment.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(comment.UserId))
            {
                throw new RutrackerException("Invalid user ID for comment.", ExceptionEventType.NotValidParameters);
            }

            if (!await _torrentRepository.ExistAsync(comment.TorrentId))
            {
                throw new RutrackerException($"Error adding comment. Torrent with id {comment.TorrentId} not found.", ExceptionEventType.NotValidParameters);
            }

            comment.CreatedAt = DateTime.Now;

            await _commentRepository.AddAsync(comment);
            await _unitOfWork.CompleteAsync();

            return comment;
        }

        public async Task<Comment> UpdateAsync(int commentId, string userId, Comment comment)
        {
            if (comment == null)
            {
                throw new RutrackerException("Not valid comment.", ExceptionEventType.NotValidParameters);
            }

            var result = await FindAsync(commentId, userId);

            result.Text = comment.Text;
            result.IsModified = true;
            result.LastModifiedAt = DateTime.Now;

            _commentRepository.Update(result);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task LikeCommentAsync(int commentId, string userId)
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
                await _likeRepository.AddAsync(new Like
                {
                    CommentId = commentId,
                    UserId = userId
                });
            }
            else
            {
                _likeRepository.Remove(like);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}