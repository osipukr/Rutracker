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

        public async Task CreateImagesContainerAsync()
        {
            var containerName = _fileStorageOptions.ImageContainer;

            await _storageService.CreateContainerAsync(containerName);
        }

        public async Task CreateTorrentContainerAsync(int torrentId)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");

            var containerName = string.Format(_fileStorageOptions.TorrentContainer, torrentId.ToString());

            await _storageService.CreateContainerAsync(containerName);
        }

        public async Task<string> UploadUserImageAsync(string userId, string mimeType, Stream imageStream)
        {
            var containerName = _fileStorageOptions.ImageContainer;
            var fileName = string.Format(_fileStorageOptions.UserImageName, userId);
            var types = _fileStorageOptions.ImageMimeTypes;
            var length = _fileStorageOptions.ImageMaxLength;

            return await UploadAsync(containerName, fileName, mimeType, imageStream, types, length);
        }

        public async Task<string> UploadTorrentImageAsync(int torrentId, string mimeType, Stream imageStream)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");

            var containerName = _fileStorageOptions.ImageContainer;
            var fileName = string.Format(_fileStorageOptions.TorrentImageName, torrentId.ToString());
            var types = _fileStorageOptions.ImageMimeTypes;
            var length = _fileStorageOptions.ImageMaxLength;

            return await UploadAsync(containerName, fileName, mimeType, imageStream, types, length);
        }

        public async Task<string> UploadTorrentFileAsync(int torrentId, string mimeType, string name, Stream fileStream)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");
            Guard.Against.NullOrWhiteSpace(name, message: "Invalid file name.");

            var containerName = string.Format(_fileStorageOptions.TorrentContainer, torrentId.ToString());
            var fileName = string.Format(_fileStorageOptions.TorrentFileName, name);
            var types = _fileStorageOptions.FileMimeTypes;
            var length = _fileStorageOptions.FileMaxLength;

            return await UploadAsync(containerName, fileName, mimeType, fileStream, types, length);
        }

        public async Task DeleteUserImageAsync(string userId)
        {
            var containerName = _fileStorageOptions.ImageContainer;
            var fileName = string.Format(_fileStorageOptions.UserImageName, userId);

            await _storageService.DeleteFileAsync(containerName, fileName);
        }

        public async Task DeleteTorrentImageAsync(int torrentId)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");

            var containerName = _fileStorageOptions.ImageContainer;
            var fileName = string.Format(_fileStorageOptions.TorrentImageName, torrentId.ToString());

            await _storageService.DeleteFileAsync(containerName, fileName);
        }

        public async Task DeleteTorrentFileAsync(int torrentId, string name)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");
            Guard.Against.NullOrWhiteSpace(name, message: "Invalid file name.");

            var containerName = string.Format(_fileStorageOptions.TorrentContainer, torrentId.ToString());
            var fileName = string.Format(_fileStorageOptions.TorrentFileName, name);

            await _storageService.DeleteFileAsync(containerName, fileName);
        }

        public async Task DeleteTorrentAsync(int torrentId)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");

            var containerName = string.Format(_fileStorageOptions.TorrentContainer, torrentId.ToString());

            await _storageService.DeleteContainerAsync(containerName);
        }

        private async Task<string> UploadAsync(
            string containerName,
            string fileName,
            string fileMimeType,
            Stream fileStream,
            string[] mimeTypes,
            long maxLength)
        {
            Guard.Against.NullOrWhiteSpace(containerName, message: "Invalid container name.");
            Guard.Against.NullOrWhiteSpace(fileName, message: "Invalid file name.");
            Guard.Against.NullOrWhiteSpace(fileMimeType, message: "Invalid mime type.");
            Guard.Against.NullNotValid(fileStream, "Invalid file stream.");

            if (mimeTypes?.Contains(fileMimeType) == false)
            {
                throw new RutrackerException($"This file type '{fileMimeType}' is not supported.", ExceptionEventTypes.NotValidParameters);
            }

            if (!fileStream.CanRead)
            {
                throw new RutrackerException("Can't read file.", ExceptionEventTypes.NotValidParameters);
            }

            if (fileStream.Length > maxLength)
            {
                throw new RutrackerException($"File too large (max {ConvertBytesToMegabytes(maxLength):F2} mb).", ExceptionEventTypes.NotValidParameters);
            }

            var path = await _storageService.UploadFileAsync(containerName, fileName, fileStream);

            await fileStream.DisposeAsync();

            return path;
        }

        private static double ConvertBytesToMegabytes(long bytes) => bytes / 1024f / 1024f;
    }
}