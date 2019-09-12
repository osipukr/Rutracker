using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Options;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IStorageService _storageService;
        private readonly FileStorageOptions _fileStorageOptions;

        public FileStorageService(IStorageService storageService, IOptions<FileStorageOptions> fileStorageOptions)
        {
            _storageService = storageService;
            _fileStorageOptions = fileStorageOptions.Value;
        }

        public async Task<string> UploadUserImageAsync(string userId, string mimeType, Stream imageStream)
        {
            var containerName = userId;
            var fileName = _fileStorageOptions.UserImageName;

            return await UploadAsync(containerName, fileName, mimeType, imageStream, _fileStorageOptions.ImageMimeTypes, _fileStorageOptions.ImageMaxLength);
        }

        public async Task<string> UploadTorrentImageAsync(int torrentId, string mimeType, Stream imageStream)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");

            var containerName = torrentId.ToString();
            var fileName = _fileStorageOptions.TorrentImageName;

            return await UploadAsync(containerName, fileName, mimeType, imageStream, _fileStorageOptions.ImageMimeTypes, _fileStorageOptions.ImageMaxLength);
        }

        public async Task<string> UploadTorrentFileAsync(int torrentId, string name, string mimeType, Stream fileStream)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");
            Guard.Against.NullOrWhiteSpace(name, message: "Invalid file name.");

            var containerName = torrentId.ToString();
            var fileName = string.Format(_fileStorageOptions.TorrentFileName, name);

            return await UploadAsync(containerName, fileName, mimeType, fileStream, _fileStorageOptions.FileMimeTypes, _fileStorageOptions.FileMaxLength);
        }

        public async Task DeleteUserImageAsync(string userId)
        {
            var containerName = userId;
            var fileName = _fileStorageOptions.UserImageName;

            await DeleteAsync(containerName, fileName);
        }

        public async Task DeleteTorrentImageAsync(int torrentId)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");

            var containerName = torrentId.ToString();
            var fileName = _fileStorageOptions.TorrentImageName;

            await DeleteAsync(containerName, fileName);
        }

        public async Task DeleteTorrentFileAsync(int torrentId, string name)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");
            Guard.Against.NullOrWhiteSpace(name, message: "Invalid file name.");

            var containerName = torrentId.ToString();
            var fileName = string.Format(_fileStorageOptions.TorrentFileName, name);

            await DeleteAsync(containerName, fileName);
        }

        public async Task DeleteTorrentFilesAsync(int torrentId)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");

            var containerName = torrentId.ToString();

            await DeleteAsync(containerName);
        }

        #region Generic

        private async Task<string> UploadAsync(string containerName, string fileName, string fileMimeType, Stream fileStream, string[] mimeTypes, long maxLength)
        {
            Guard.Against.NullOrWhiteSpace(containerName, message: "Invalid container name.");
            Guard.Against.NullOrWhiteSpace(fileName, message: "Invalid file name.");
            Guard.Against.NullOrWhiteSpace(fileMimeType, message: "Invalid mime type.");
            Guard.Against.NullNotValid(fileStream, "Invalid file stream.");

            if (!mimeTypes.Contains(fileMimeType))
            {
                throw new RutrackerException($"This file type '{fileMimeType}' is not supported.", ExceptionEventTypes.NotValidParameters);
            }

            if (!fileStream.CanRead)
            {
                throw new RutrackerException("Can't read file.", ExceptionEventTypes.NotValidParameters);
            }

            if (fileStream.Length > maxLength)
            {
                throw new RutrackerException($"File too large (max {ConvertBytesToMegabytes(maxLength)} mb).", ExceptionEventTypes.NotValidParameters);
            }

            var path = await _storageService.UploadFileAsync(containerName, fileName, fileStream);

            await fileStream.DisposeAsync();

            return path;
        }

        private async Task DeleteAsync(string containerName)
        {
            Guard.Against.NullOrWhiteSpace(containerName, message: "Invalid container name.");

            if (!await _storageService.DeleteContainerAsync(containerName))
            {
                throw new RutrackerException("Unable to delete container.", ExceptionEventTypes.NotValidParameters);
            }
        }

        private async Task DeleteAsync(string containerName, string fileName)
        {
            Guard.Against.NullOrWhiteSpace(containerName, message: "Invalid container name.");
            Guard.Against.NullOrWhiteSpace(fileName, message: "Invalid file name.");

            if (!await _storageService.DeleteFileAsync(containerName, fileName))
            {
                throw new RutrackerException("Unable to delete user image.", ExceptionEventTypes.NotValidParameters);
            }
        }

        #endregion

        private static double ConvertBytesToMegabytes(long bytes) => bytes / 1024f / 1024f;
    }
}