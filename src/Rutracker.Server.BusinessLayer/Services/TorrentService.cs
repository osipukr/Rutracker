using System;
using System.Collections.Generic;
using System.IO;
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
    public class TorrentService : ITorrentService
    {
        private readonly ITorrentRepository _torrentRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _unitOfWork;

        public TorrentService(
            ITorrentRepository torrentRepository,
            ISubcategoryRepository subcategoryRepository,
            IFileStorageService fileStorageService,
            IUnitOfWork unitOfWork)
        {
            _torrentRepository = torrentRepository;
            _subcategoryRepository = subcategoryRepository;
            _fileStorageService = fileStorageService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Tuple<IEnumerable<Torrent>, int>> ListAsync(int page, int pageSize, int? categoryId, int? subcategoryId, string search)
        {
            Guard.Against.LessOne(page, "Invalid page.");
            Guard.Against.OutOfRange(pageSize, 1, 100, "The page size is out of range (1 - 100).");

            if (categoryId.HasValue)
            {
                Guard.Against.LessOne(categoryId.Value, "Invalid category id.");
            }

            if (subcategoryId.HasValue)
            {
                Guard.Against.LessOne(subcategoryId.Value, "Invalid subcategory id.");
            }

            var query = _torrentRepository.GetAll(torrent =>
                (!subcategoryId.HasValue || torrent.SubcategoryId == subcategoryId) &&
                (!categoryId.HasValue || torrent.Subcategory.CategoryId == categoryId) &&
                (string.IsNullOrWhiteSpace(search) || torrent.Name.Contains(search)))
                .OrderBy(torrent => torrent.CreatedAt);

            var torrents = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            Guard.Against.NullNotFound(torrents, "The torrents not found.");

            var count = await query.CountAsync();

            return Tuple.Create<IEnumerable<Torrent>, int>(torrents, count);
        }

        public async Task<IEnumerable<Torrent>> PopularAsync(int count)
        {
            Guard.Against.OutOfRange(count, 1, 100, "The count is out of range (1 - 100).");

            var torrents = await _torrentRepository.GetAll()
                .OrderByDescending(x => x.Comments.Count)
                .Take(count)
                .ToListAsync();

            Guard.Against.NullNotFound(torrents, "The torrents not found.");

            return torrents;
        }

        public async Task<Torrent> FindAsync(int id)
        {
            Guard.Against.LessOne(id, "Invalid torrent id.");

            var torrent = await _torrentRepository.GetAsync(id);

            Guard.Against.NullNotFound(torrent, $"The torrent with id '{id}' not found.");

            return torrent;
        }

        public async Task<Torrent> FindAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, "Invalid torrent id.");
            Guard.Against.LessOne(id, "Invalid user id.");

            var torrent = await _torrentRepository.GetAsync(id);

            Guard.Against.NullNotFound(torrent, $"The torrent with id '{id}' not found.");

            return torrent;
        }

        public async Task<Torrent> AddAsync(Torrent torrent)
        {
            Guard.Against.NullNotValid(torrent, "Invalid torrent.");
            Guard.Against.NullOrWhiteSpace(torrent.Name, message: "The torrent must contain a name.");
            Guard.Against.NullOrWhiteSpace(torrent.Description, message: "The torrent must contain a description.");
            Guard.Against.LessOne(torrent.SubcategoryId, "Invalid subcategory id.");
            Guard.Against.NullOrWhiteSpace(torrent.UserId, message: "Invalid user id.");

            if (!await _subcategoryRepository.ExistAsync(torrent.SubcategoryId))
            {
                throw new RutrackerException($"The subcategory with id {torrent.SubcategoryId} not found.", ExceptionEventTypes.NotValidParameters);
            }

            torrent.CreatedAt = DateTime.UtcNow;

            torrent = await _torrentRepository.AddAsync(torrent);
            await _unitOfWork.CompleteAsync();

            return torrent;
        }

        public async Task<Torrent> ChangeImageAsync(int id, string userId, string imageUrl)
        {
            Guard.Against.NullOrWhiteSpace(imageUrl, message: "Invalid link to the picture.");

            var torrent = await FindAsync(id, userId);

            return await UpdateImageAsync(torrent, imageUrl);
        }

        public async Task<Torrent> ChangeImageAsync(int id, string userId, string imageMimeType, Stream imageStream)
        {
            var torrent = await FindAsync(id, userId);
            var path = await _fileStorageService.UploadTorrentImageAsync(id, imageMimeType, imageStream);

            return await UpdateImageAsync(torrent, path);
        }

        public async Task<Torrent> DeleteImageAsync(int id, string userId)
        {
            var torrent = await FindAsync(id, userId);
            await _fileStorageService.DeleteTorrentImageAsync(id);

            return await UpdateImageAsync(torrent, imageUrl: null);
        }

        public async Task<Torrent> UpdateAsync(int id, string userId, Torrent torrent)
        {
            Guard.Against.NullNotValid(torrent, "Invalid torrent.");
            Guard.Against.NullNotValid(torrent.Name, "The torrent must contain a name.");
            Guard.Against.NullNotValid(torrent.Description, "The torrent must contain a description.");

            var result = await FindAsync(id, userId);

            result.Name = torrent.Name;
            result.Description = torrent.Description;
            result.LastUpdatedAt = DateTime.UtcNow;

            result = _torrentRepository.Update(result);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<Torrent> DeleteAsync(int id)
        {
            var torrent = await FindAsync(id);

            torrent = _torrentRepository.Remove(torrent);

            await _unitOfWork.CompleteAsync();

            return torrent;
        }

        private async Task<Torrent> UpdateImageAsync(Torrent torrent, string imageUrl)
        {
            torrent.ImageUrl = imageUrl;

            torrent = _torrentRepository.Update(torrent);

            await _unitOfWork.CompleteAsync();

            return torrent;
        }
    }
}