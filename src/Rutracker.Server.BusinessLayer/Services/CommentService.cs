using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;
using Rutracker.Server.BusinessLayer.Services.Base;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Interfaces.Base;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class CommentService : Service, ICommentService
    {
        private readonly IDateService _dateService;
        private readonly ITorrentRepository _torrentRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;

        public CommentService(IUnitOfWork<RutrackerContext> unitOfWork, IDateService dateService) : base(unitOfWork)
        {
            _dateService = dateService;

            _torrentRepository = _unitOfWork.GetRepository<ITorrentRepository>();
            _commentRepository = _unitOfWork.GetRepository<ICommentRepository>();
            _likeRepository = _unitOfWork.GetRepository<ILikeRepository>();
        }

        public async Task<IPagedList<Comment>> ListAsync(ICommentFilter filter)
        {
            Guard.Against.OutOfRange(filter.Page, Constants.Filter.PageRangeFrom, Constants.Filter.PageRangeTo, Resources.Page_InvalidPageNumber);
            Guard.Against.OutOfRange(filter.PageSize, Constants.Filter.PageSizeRangeFrom, Constants.Filter.PageSizeRangeTo, Resources.PageSize_InvalidPageSizeNumber);
            Guard.Against.LessOne(filter.TorrentId, Resources.Torrent_InvalidId_ErrorMessage);

            var query = _commentRepository.GetAll(x => x.TorrentId == filter.TorrentId)
                .OrderByDescending(x => x.Likes.Count)
                .ThenByDescending(x => x.AddedDate);

            var pagedList = await ApplyFilterAsync(query, filter);

            Guard.Against.NullNotFound(pagedList.Items, Resources.Comment_NotFoundList_ErrorMessage);

            return pagedList;
        }

        public async Task<Comment> FindAsync(int id)
        {
            Guard.Against.LessOne(id, Resources.Comment_InvalidId_ErrorMessage);

            var comment = await _commentRepository.GetAsync(id);

            Guard.Against.NullNotFound(comment, string.Format(Resources.Comment_NotFoundById_ErrorMessage, id));

            return comment;
        }

        public async Task<Comment> FindAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, Resources.Comment_InvalidId_ErrorMessage);
            Guard.Against.NullString(userId, Resources.User_InvalidId_ErrorMessage);

            var comment = await _commentRepository.GetAsync(x => x.Id == id && x.UserId == userId);

            Guard.Against.NullNotFound(comment, string.Format(Resources.Comment_NotFoundById_ErrorMessage, id));

            return comment;
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            Guard.Against.NullInvalid(comment, Resources.Comment_Invalid_ErrorMessage);
            Guard.Against.NullString(comment.Text, Resources.Comment_InvalidText_ErrorMessage);
            Guard.Against.NullString(comment.UserId, Resources.User_InvalidId_ErrorMessage);
            Guard.Against.LessOne(comment.TorrentId, Resources.Torrent_InvalidId_ErrorMessage);

            if (!await _torrentRepository.ExistAsync(comment.TorrentId))
            {
                throw new RutrackerException(
                    string.Format(Resources.Torrent_NotFoundById_ErrorMessage, comment.TorrentId),
                    ExceptionEventTypes.InvalidParameters);
            }

            comment.AddedDate = _dateService.Now();

            var result = await _commentRepository.AddAsync(comment);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<Comment> UpdateAsync(int id, string userId, Comment comment)
        {
            Guard.Against.NullInvalid(comment, Resources.Comment_Invalid_ErrorMessage);
            Guard.Against.NullString(comment.Text, message: Resources.Comment_InvalidText_ErrorMessage);

            var result = await FindAsync(id, userId);

            result.Text = comment.Text;
            result.ModifiedDate = _dateService.Now();

            _commentRepository.Update(result);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<Comment> DeleteAsync(int id, string userId)
        {
            var comment = await FindAsync(id, userId);

            _commentRepository.Delete(comment);

            await _unitOfWork.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> LikeCommentAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, Resources.Comment_InvalidId_ErrorMessage);
            Guard.Against.NullString(userId, Resources.User_InvalidId_ErrorMessage);

            var comment = await _commentRepository.GetAsync(id);

            if (comment == null)
            {
                throw new RutrackerException(
                    string.Format(Resources.Comment_NotFoundById_ErrorMessage, id),
                    ExceptionEventTypes.InvalidParameters);
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
                _likeRepository.Delete(like);
            }

            await _unitOfWork.SaveChangesAsync();

            return comment;
        }
    }
}