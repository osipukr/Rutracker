using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;
using Rutracker.Server.BusinessLayer.Services.Base;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Interfaces.Base;
using File = Rutracker.Server.DataAccessLayer.Entities.File;
using FileOptions = Rutracker.Server.BusinessLayer.Options.FileOptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class FileService : Service, IFileService
    {
        private readonly IStorageService _storageService;

        private readonly IFileRepository _fileRepository;
        private readonly ITorrentRepository _torrentRepository;

        private readonly FileOptions _fileOptions;

        public FileService(
            IUnitOfWork<RutrackerContext> unitOfWork,
            IStorageService storageService,
            IOptions<FileOptions> fileOptions) : base(unitOfWork)
        {
            _storageService = storageService;

            _fileOptions = fileOptions.Value;

            _fileRepository = _unitOfWork.GetRepository<IFileRepository>();
            _torrentRepository = _unitOfWork.GetRepository<ITorrentRepository>();
        }

        public async Task<IEnumerable<File>> ListAsync(int torrentId)
        {
            Guard.Against.LessOne(torrentId, Resources.Torrent_InvalidId_ErrorMessage);

            var files = await _fileRepository.GetAll(x => x.TorrentId == torrentId)
                .OrderByDescending(x => x.Size)
                .ThenByDescending(x => x.Name)
                .ToListAsync();

            var message = string.Format(Resources.File_NotFoundListByTorrentId_ErrorMessage, torrentId);

            Guard.Against.NullNotFound(files, message);

            return files;
        }

        public async Task<File> AddAsync(int torrentId, IFormFile file)
        {
            Guard.Against.NullInvalid(file, "Invalid file");

            if (string.IsNullOrWhiteSpace(file.FileName))
            {
                throw new RutrackerException(
                    "The file name cannot be empty.",
                    ExceptionEventTypes.InvalidParameters);
            }

            if (file.Length > _fileOptions.MaxSize)
            {
                throw new RutrackerException(
                    $"File '{file.Name}' is too large (more {_fileOptions.MaxSize} bytes).",
                    ExceptionEventTypes.InvalidParameters);
            }

            var torrent = await _torrentRepository.GetAsync(torrentId);

            if (torrent == null)
            {
                throw new RutrackerException(
                    $"The torrent with '{torrentId}' not found",
                    ExceptionEventTypes.InvalidParameters);
            }

            if (torrent.IsStockTorrent)
            {
                throw new RutrackerException(
                    "Cannot add the file to the stock torrent.",
                    ExceptionEventTypes.InvalidParameters);
            }

            var result = await _fileRepository.AddAsync(new File
            {
                Name = file.FileName,
                BlobName = $"{Path.GetFileNameWithoutExtension(file.FileName)}-{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}",
                Size = file.Length,
                Type = file.ContentType,
                TorrentId = torrentId
            });

            await _unitOfWork.SaveChangesAsync();

            var containerName = torrentId.ToString().PadLeft(10, '0');
            var fileName = result.BlobName;
            var contentType = result.Type;
            var stream = file.OpenReadStream();

            var url = await _storageService.UploadFileAsync(containerName, fileName, contentType, stream);

            result.Url = url;
            torrent.Size += result.Size;

            _fileRepository.Update(result);
            _torrentRepository.Update(torrent);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<File> FindAsync(int id)
        {
            Guard.Against.LessOne(id, Resources.File_InvalidId_ErrorMessage);

            var file = await _fileRepository.GetAsync(id);

            Guard.Against.NullNotFound(file, string.Format(Resources.File_NotFoundById_ErrorMessage, id));

            return file;
        }

        public async Task<File> Delete(int id)
        {
            var file = await FindAsync(id);

            _fileRepository.Delete(file);

            var containerName = file.TorrentId.ToString().PadLeft(10, '0');
            var fileName = file.BlobName;

            await _storageService.DeleteFileAsync(containerName, fileName);

            await _unitOfWork.SaveChangesAsync();

            return file;
        }
    }
}