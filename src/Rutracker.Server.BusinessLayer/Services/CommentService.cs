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
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(ICommentRepository commentRepository, ITorrentRepository torrentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _torrentRepository = torrentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Comment>> ListAsync(int torrentId, int count)
        {
            Guard.Against.LessOne(torrentId, $"The {nameof(torrentId)} is less than 1.");
            Guard.Against.LessOne(count, $"The {nameof(count)} is less than 1.");

            var comments = await _commentRepository.GetAll(x => x.TorrentId == torrentId)
                .OrderByDescending(x => x.Likes.Count)
                .Take(count)
                .ToListAsync();

            Guard.Against.NullNotFound(comments, "Comments not found.");

            return comments;
        }

        public async Task<Comment> FindAsync(int commentId, string userId)
        {
            Guard.Against.LessOne(commentId, $"The {nameof(commentId)} is less than 1.");
            Guard.Against.NullOrWhiteSpace(userId, message: $"The {nameof(userId)} is null or white space.");

            var comment = await _commentRepository.GetAsync(x => x.Id == commentId && x.UserId == userId);

            Guard.Against.NullNotFound(comment, "Comment not found.");

            return comment;
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            Guard.Against.NullNotValid(comment, "Not valid comment.");
            Guard.Against.NullOrWhiteSpace(comment.Text, message: "The comment must contain text.");
            Guard.Against.LessOne(comment.TorrentId, "Invalid torrent ID for comment.");
            Guard.Against.NullOrWhiteSpace(comment.UserId, message: "Invalid user ID for comment.");

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
            Guard.Against.NullNotValid(comment, "Not valid comment.");

            var result = await FindAsync(commentId, userId);

            result.Text = comment.Text;
            result.IsModified = true;
            result.LastModifiedAt = DateTime.Now;

            _commentRepository.Update(result);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Comment> DeleteAsync(int commentId, string userId)
        {
            var comment = await FindAsync(commentId, userId);

            _commentRepository.Remove(comment);

            await _unitOfWork.CompleteAsync();

            return comment;
        }
    }
}