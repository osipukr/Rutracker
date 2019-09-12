using System.IO;
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
        private IStorageService _storageService;
        private FileStorageOptions _fileStorageOptions;

        public FileStorageService(IStorageService storageService, IOptions<FileStorageOptions> fileStorageOptions)
        {
            _storageService = storageService;
            _fileStorageOptions = fileStorageOptions.Value;
        }

        public async Task<string> UploadUserImageAsync(string userId, Stream imageStream)
        {
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");
            Guard.Against.NullNotValid(imageStream, "Invalid image stream.");

            if (!imageStream.CanRead)
            {
                throw new RutrackerException("Can't read image.", ExceptionEventTypes.NotValidParameters);
            }

            // validation image here

            var path = await _storageService.UploadFileAsync(userId, _fileStorageOptions.UserImageName, imageStream);

            await imageStream.DisposeAsync();

            return path;
        }

        public async Task<string> UploadTorrentImageAsync(int torrentId, Stream imageStream)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> UploadTorrentFileAsync(int torrentId, Stream fileStream)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteUserImageAsync(string userId)
        {
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");

            if (!await _storageService.DeleteFileAsync(userId, _fileStorageOptions.UserImageName))
            {
                throw new RutrackerException("Unable to delete user image.", ExceptionEventTypes.NotValidParameters);
            }
        }
    }
}