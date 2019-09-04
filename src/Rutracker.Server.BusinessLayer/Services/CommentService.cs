using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
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
            Guard.Against.LessOne(torrentId, $"The {nameof(torrentId)} is less than 1.");

            var comments = await _commentRepository.GetAll(x => x.TorrentId == torrentId)
                .OrderByDescending(x => x.Likes.Count)
                .ToListAsync();

            Guard.Against.NullNotFound(comments, "Comments not found.");

            return comments;
        }

        public async Task<Comment> FindAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, $"The {nameof(id)} is less than 1.");
            Guard.Against.NullOrWhiteSpace(userId, message: $"The {nameof(userId)} is null or white space.");

            var comment = await _commentRepository.GetAsync(x => x.Id == id && x.UserId == userId);

            Guard.Against.NullNotFound(comment, "Comment not found.");

            return comment;
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            ThrowIfInvalidCommentModel(comment);

            if (!await _torrentRepository.ExistAsync(comment.TorrentId))
            {
                throw new RutrackerException($"Error adding comment. Torrent with id {comment.TorrentId} not found.", ExceptionEventType.NotValidParameters);
            }

            comment.CreatedAt = DateTime.Now;

            await _commentRepository.AddAsync(comment);
            await _unitOfWork.CompleteAsync();

            return comment;
        }

        public async Task<Comment> UpdateAsync(int id, string userId, Comment comment)
        {
            ThrowIfInvalidCommentModel(comment);

            var result = await FindAsync(id, userId);

            result.Text = comment.Text;
            result.IsModified = true;
            result.LastModifiedAt = DateTime.Now;

            _commentRepository.Update(result);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Comment> DeleteAsync(int id, string userId)
        {
            var comment = await FindAsync(id, userId);

            _commentRepository.Remove(comment);

            await _unitOfWork.CompleteAsync();

            return comment;
        }

        public async Task<Comment> LikeCommentAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, $"The {nameof(id)} is less than 1.");
            Guard.Against.NullOrWhiteSpace(userId, message: $"The {nameof(userId)} is null or white space.");

            var comment = await _commentRepository.GetAsync(id);
            var like = await _likeRepository.GetAsync(x => x.CommentId == id && x.UserId == userId);

            if (like != null)
            {
                _likeRepository.Remove(like);
            }
            else if (comment == null)
            {
                throw new RutrackerException("Not valid comment ID.", ExceptionEventType.NotValidParameters);
            }
            else
            {
                await _likeRepository.AddAsync(new Like
                {
                    CommentId = id,
                    UserId = userId
                });
            }

            await _unitOfWork.CompleteAsync();

            return comment;
        }

        private static void ThrowIfInvalidCommentModel(Comment comment)
        {
            Guard.Against.NullNotValid(comment, "Not valid comment.");
            Guard.Against.NullOrWhiteSpace(comment.Text, message: "The comment must contain text.");
        }
    }
}