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

        public async Task<Tuple<IEnumerable<Comment>, int>> ListAsync(int page, int pageSize, int torrentId)
        {
            Guard.Against.LessOne(page, $"The {nameof(page)} is less than 1.");
            Guard.Against.OutOfRange(pageSize, rangeFrom: 1, rangeTo: 100, $"The {nameof(pageSize)} is out of range ({1} - {100}).");
            Guard.Against.LessOne(torrentId, $"The {nameof(torrentId)} is less than 1.");

            var query = _commentRepository.GetAll(x => x.TorrentId == torrentId)
                .OrderByDescending(x => x.Likes.Count)
                .ThenByDescending(x => x.CreatedAt);

            var comments = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            Guard.Against.NullNotFound(comments, "The comments not found.");

            var count = await query.CountAsync();

            return Tuple.Create<IEnumerable<Comment>, int>(comments, count);
        }

        public async Task<Comment> FindAsync(int id, string userId)
        {
            ThrowIfInvalidParameters(id, userId);

            var comment = await _commentRepository.GetAsync(x => x.Id == id && x.UserId == userId);

            Guard.Against.NullNotFound(comment, $"The comment with id '{id}' not found.");

            return comment;
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            ThrowIfInvalidComment(comment);

            if (!await _torrentRepository.ExistAsync(comment.TorrentId))
            {
                throw new RutrackerException($"The torrent with id '{comment.TorrentId}' not found.", ExceptionEventType.NotValidParameters);
            }

            comment.CreatedAt = DateTime.UtcNow;

            await _commentRepository.AddAsync(comment);
            await _unitOfWork.CompleteAsync();

            return comment;
        }

        public async Task<Comment> UpdateAsync(int id, string userId, Comment comment)
        {
            ThrowIfInvalidComment(comment);

            var result = await FindAsync(id, userId);

            result.Text = comment.Text;
            result.IsModified = true;
            result.LastModifiedAt = DateTime.UtcNow;

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
            ThrowIfInvalidParameters(id, userId);

            var comment = await _commentRepository.GetAsync(id);

            if (comment == null)
            {
                throw new RutrackerException($"The comment with id '{id}' not found.", ExceptionEventType.NotValidParameters);
            }

            var like = await _likeRepository.GetAsync(x => x.CommentId == id && x.UserId == userId);

            if (like == null)
            {
                await _likeRepository.AddAsync(new Like
                {
                    CommentId = id,
                    UserId = userId
                });
            }
            else
            {
                _likeRepository.Remove(like);
            }

            await _unitOfWork.CompleteAsync();

            return comment;
        }

        private static void ThrowIfInvalidParameters(int id, string userId)
        {
            Guard.Against.LessOne(id, $"The {nameof(id)} is less than 1.");
            Guard.Against.NullOrWhiteSpace(userId, message: $"The {nameof(userId)} is null or white space.");
        }

        private static void ThrowIfInvalidComment(Comment comment)
        {
            Guard.Against.NullNotValid(comment, "Invalid comment model.");
            Guard.Against.NullOrWhiteSpace(comment.Text, message: "The comment must contain text.");
        }
    }
}