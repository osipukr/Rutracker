using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly ITorrentRepository _torrentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FileService(IFileRepository fileRepository, ITorrentRepository torrentRepository, IUnitOfWork unitOfWork)
        {
            _fileRepository = fileRepository;
            _torrentRepository = torrentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<File>> ListAsync(int torrentId)
        {
            Guard.Against.LessOne(torrentId, "Invalid torrent id.");

            var files = await _fileRepository.GetAll(x => x.TorrentId == torrentId).ToListAsync();

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

        public async Task<File> AddAsync(string userId, File file)
        {
            Guard.Against.NullOrWhiteSpace(userId, message: "Invalid user id.");
            Guard.Against.NullNotValid(file, "Invalid file.");
            Guard.Against.NullOrWhiteSpace(file.Name, message: "The file must contain a name.");
            Guard.Against.NullOrWhiteSpace(file.Url, message: "The file must contain a url.");
            Guard.Against.LessOne(file.TorrentId, message: "Invalid torrent id.");

            if (!await _torrentRepository.ExistAsync(x => x.Id == file.TorrentId && x.UserId == userId))
            {
                throw new RutrackerException($"The torrent with id '{file.TorrentId}' not found.", ExceptionEventTypes.NotValidParameters);
            }

            file = await _fileRepository.AddAsync(file);
            await _unitOfWork.CompleteAsync();

            return file;
        }

        public async Task<File> DeleteAsync(int id, string userId)
        {
            var file = await FindAsync(id, userId);

            file = _fileRepository.Remove(file);

            await _unitOfWork.CompleteAsync();

            return file;
        }
    }
}