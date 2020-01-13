using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;
using Rutracker.Server.BusinessLayer.Services.Base;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class TorrentService : BaseService, ITorrentService
    {
        private readonly ITorrentRepository _torrentRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFileStorageService _fileStorageService;

        public TorrentService(
            ITorrentRepository torrentRepository,
            ISubcategoryRepository subcategoryRepository,
            IFileStorageService fileStorageService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _torrentRepository = torrentRepository;
            _subcategoryRepository = subcategoryRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<Tuple<IEnumerable<Torrent>, int>> ListAsync(int page, int pageSize, int? categoryId, int? subcategoryId, string search)
        {
            Guard.Against.OutOfRange(page, Resources.Page_RangeFrom, Resources.Page_RangeTo, Resources.Page_InvalidPageNumber);

            var message = string.Format(Resources.PageSize_InvalidPageSizeNumber, nameof(pageSize), Resources.PageSize_RangeFrom, Resources.PageSize_RangeTo);

            Guard.Against.OutOfRange(pageSize, Resources.PageSize_RangeFrom, Resources.PageSize_RangeTo, message);

            var query = _torrentRepository.GetAll(x => string.IsNullOrWhiteSpace(search) || x.Name.Contains(search));

            if (categoryId.HasValue)
            {
                Guard.Against.LessOne(categoryId.Value, Resources.Torrent_InvalidCategoryId_ErrorMessage);

                query = query.Where(x => x.Subcategory.CategoryId == categoryId.Value);
            }

            if (subcategoryId.HasValue)
            {
                Guard.Against.LessOne(subcategoryId.Value, Resources.Torrent_InvalidSubcategoryId_ErrorMessage);

                query = query.Where(x => x.SubcategoryId == subcategoryId.Value);
            }

            var torrents = await query.OrderBy(x => x.AddedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Guard.Against.NullNotFound(torrents, Resources.Torrent_NotFoundList_ErrorMessage);

            var count = await query.CountAsync();

            return Tuple.Create<IEnumerable<Torrent>, int>(torrents, count);
        }

        public async Task<IEnumerable<Torrent>> PopularAsync(int count)
        {
            var message = string.Format(Resources.PageSize_InvalidPageSizeNumber, nameof(count), Resources.PageSize_RangeFrom, Resources.PageSize_RangeTo);

            Guard.Against.OutOfRange(count, Resources.PageSize_RangeFrom, Resources.PageSize_RangeTo, message);

            var torrents = await _torrentRepository.GetAll()
                .OrderByDescending(x => x.Comments.Count)
                .Take(count)
                .ToListAsync();

            Guard.Against.NullNotFound(torrents, Resources.Torrent_NotFoundList_ErrorMessage);

            return torrents;
        }

        public async Task<Torrent> FindAsync(int id)
        {
            Guard.Against.LessOne(id, Resources.Torrent_InvalidId_ErrorMessage);

            var torrent = await _torrentRepository.GetAsync(id);

            Guard.Against.NullNotFound(torrent, string.Format(Resources.Torrent_NotFoundById_ErrorMessage, id));

            return torrent;
        }

        public async Task<Torrent> FindAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, Resources.Torrent_InvalidId_ErrorMessage);
            Guard.Against.LessOne(id, Resources.User_InvalidId_ErrorMessage);

            var torrent = await _torrentRepository.GetAsync(id);

            Guard.Against.NullNotFound(torrent, string.Format(Resources.Torrent_NotFoundById_ErrorMessage, id));

            return torrent;
        }

        public async Task<Torrent> AddAsync(Torrent torrent)
        {
            Guard.Against.NullNotValid(torrent, Resources.Torrent_Invalid_ErrorMessage);
            Guard.Against.NullString(torrent.Name, Resources.Torrent_InvalidName_ErrorMessage);
            Guard.Against.NullString(torrent.Description, Resources.Torrent_InvalidDescription_ErrorMessage);
            Guard.Against.NullString(torrent.Content, Resources.Torrent_InvalidContent_ErrorMessage);
            Guard.Against.LessOne(torrent.SubcategoryId, Resources.Torrent_InvalidSubcategoryId_ErrorMessage);
            Guard.Against.NullString(torrent.UserId, Resources.User_InvalidId_ErrorMessage);

            if (!await _subcategoryRepository.ExistAsync(torrent.SubcategoryId))
            {
                var message = string.Format(Resources.Subcategory_NotFoundById_ErrorMessage, torrent.SubcategoryId);

                throw new RutrackerException(message, ExceptionEventTypes.InvalidParameters);
            }

            torrent.AddedDate = DateTime.UtcNow;

            await _torrentRepository.AddAsync(torrent);
            await _unitOfWork.CompleteAsync();
            await _fileStorageService.CreateTorrentContainerAsync(torrent.Id);

            return torrent;
        }

        public async Task<Torrent> ChangeImageAsync(int id, string userId, string imageUrl)
        {
            Guard.Against.IsValidUrl(imageUrl, Resources.Torrent_InvalidImageUrl_ErrorMessage);

            var torrent = await FindAsync(id, userId);

            torrent.ImageUrl = imageUrl;

            _torrentRepository.Update(torrent);

            await _unitOfWork.CompleteAsync();

            return torrent;
        }

        public async Task<Torrent> ChangeImageAsync(int id, string userId, string imageMimeType, Stream imageStream)
        {
            var torrent = await FindAsync(id, userId);

            await _fileStorageService.CreateImagesContainerAsync();

            torrent.ImageUrl = await _fileStorageService.UploadTorrentImageAsync(id, imageMimeType, imageStream);

            _torrentRepository.Update(torrent);

            await _unitOfWork.CompleteAsync();

            return torrent;
        }

        public async Task<Torrent> DeleteImageAsync(int id, string userId)
        {
            var torrent = await FindAsync(id, userId);

            await _fileStorageService.DeleteTorrentImageAsync(id);

            torrent.ImageUrl = null;

            _torrentRepository.Update(torrent);

            await _unitOfWork.CompleteAsync();

            return torrent;
        }

        public async Task<Torrent> UpdateAsync(int id, string userId, Torrent torrent)
        {
            Guard.Against.NullNotValid(torrent, Resources.Torrent_Invalid_ErrorMessage);
            Guard.Against.NullNotValid(torrent.Name, Resources.Torrent_InvalidName_ErrorMessage);
            Guard.Against.NullNotValid(torrent.Description, Resources.Torrent_InvalidDescription_ErrorMessage);

            var result = await FindAsync(id, userId);

            result.Name = torrent.Name;
            result.Description = torrent.Description;
            result.Content = torrent.Content;
            result.ModifiedDate = DateTime.UtcNow;

            _torrentRepository.Update(result);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Torrent> DeleteAsync(int id, string userId)
        {
            var torrent = await FindAsync(id, userId);

            _torrentRepository.Remove(torrent);

            await _unitOfWork.CompleteAsync();
            await _fileStorageService.DeleteTorrentAsync(id);

            return torrent;
        }
    }
}