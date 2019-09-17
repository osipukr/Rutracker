using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;
using File = Rutracker.Server.DataAccessLayer.Entities.File;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly ITorrentRepository _torrentRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _unitOfWork;

        public FileService(
            IFileRepository fileRepository,
            ITorrentRepository torrentRepository,
            IFileStorageService fileStorageService,
            IUnitOfWork unitOfWork)
        {
            _fileRepository = fileRepository;
            _torrentRepository = torrentRepository;
            _fileStorageService = fileStorageService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<File>> ListAsync(int torrentId)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");

            var files = await _fileRepository.GetAll(x => x.TorrentId == torrentId)
                .OrderByDescending(x => x.Size)
                .ThenByDescending(x => x.Id)
                .ToListAsync();

            Guard.Against.NullNotFound(files, $"The files with torrent id '{torrentId}' not found.");

            return files;
        }

        public async Task<File> FindAsync(int id)
        {
            Guard.Against.LessOne(id, "Invalid file id.");

            var file = await _fileRepository.GetAsync(id);

            Guard.Against.NullNotFound(file, $"The file with id '{id}' not found.");

            return file;
        }

        public async Task<File> FindAsync(int id, string userId)
        {
            Guard.Against.LessOne(id, "Invalid file id.");
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");

            var file = await _fileRepository.GetAsync(x => x.Id == id && x.Torrent.UserId == userId);

            Guard.Against.NullNotFound(file, $"The file with id '{id}' not found.");

            return file;
        }

        public async Task<File> AddAsync(string userId, int torrentId, string mimeType, string fileName, Stream fileStream)
        {
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");

            if (!await _torrentRepository.ExistAsync(x => x.Id == torrentId && x.UserId == userId))
            {
                throw new RutrackerException($"The torrent with id '{torrentId}' not found.", ExceptionEventTypes.NotValidParameters);
            }

            var name = fileName.ToLowerInvariant();
            var type = mimeType.ToLowerInvariant();
            var path = await _fileStorageService.UploadTorrentFileAsync(torrentId, type, name, fileStream);
            var file = await _fileRepository.AddAsync(new File
            {
                Name = name,
                Size = fileStream.Length,
                Type = type,
                Url = path,
                TorrentId = torrentId
            });

            await _unitOfWork.CompleteAsync();

            return file;
        }

        public async Task<File> DeleteAsync(int id, string userId)
        {
            var file = await FindAsync(id, userId);

            await _fileStorageService.DeleteTorrentFileAsync(file.TorrentId, file.Name);

            file = _fileRepository.Remove(file);

            await _unitOfWork.CompleteAsync();

            return file;
        }

        public async Task<Tuple<File, Stream>> DownloadAsync(int id)
        {
            var file = await FindAsync(id);
            var stream = await _fileStorageService.DownloadTorrentFileAsync(file.TorrentId, file.Name);

            return Tuple.Create(file, stream);
        }
    }
}