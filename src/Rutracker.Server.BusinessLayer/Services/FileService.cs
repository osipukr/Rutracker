using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Properties;
using Rutracker.Server.BusinessLayer.Services.Base;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Interfaces.Base;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class FileService : Service, IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IUnitOfWork<RutrackerContext> unitOfWork) : base(unitOfWork)
        {
            _fileRepository = _unitOfWork.GetRepository<IFileRepository>();
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

        public async Task<File> FindAsync(int id)
        {
            Guard.Against.LessOne(id, Resources.File_InvalidId_ErrorMessage);

            var file = await _fileRepository.GetAsync(id);

            Guard.Against.NullNotFound(file, string.Format(Resources.File_NotFoundById_ErrorMessage, id));

            return file;
        }
    }
}