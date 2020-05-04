using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using ICSharpCode.SharpZipLib.Zip;
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
    public class TorrentService : Service, ITorrentService
    {
        private readonly IDateService _dateService;
        private readonly IStorageService _storageService;

        private readonly ITorrentRepository _torrentRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public TorrentService(
            IUnitOfWork<RutrackerContext> unitOfWork,
            IDateService dateService,
            IStorageService storageService) : base(unitOfWork)
        {
            _dateService = dateService;
            _storageService = storageService;

            _torrentRepository = _unitOfWork.GetRepository<ITorrentRepository>();
            _subcategoryRepository = _unitOfWork.GetRepository<ISubcategoryRepository>();
        }

        public async Task<IPagedList<Torrent>> ListAsync(ITorrentFilter filter)
        {
            Guard.Against.OutOfRange(filter.Page, Constants.Filter.PageRangeFrom, Constants.Filter.PageRangeTo, Resources.Page_InvalidPageNumber);

            var message = string.Format(Resources.PageSize_InvalidPageSizeNumber, Constants.Filter.PageRangeFrom, Constants.Filter.PageRangeTo);

            Guard.Against.OutOfRange(filter.PageSize, Constants.Filter.PageRangeFrom, Constants.Filter.PageRangeTo, message);

            var query = _torrentRepository.GetAll(x => string.IsNullOrWhiteSpace(filter.Search) || x.Name.Contains(filter.Search));

            if (filter.CategoryId.HasValue)
            {
                Guard.Against.LessOne(filter.CategoryId.Value, Resources.Torrent_InvalidCategoryId_ErrorMessage);

                query = query.Where(x => x.Subcategory.CategoryId == filter.CategoryId.Value);
            }

            if (filter.SubcategoryId.HasValue)
            {
                Guard.Against.LessOne(filter.SubcategoryId.Value, Resources.Torrent_InvalidSubcategoryId_ErrorMessage);

                query = query.Where(x => x.SubcategoryId == filter.SubcategoryId.Value);
            }

            query = query.OrderBy(x => x.AddedDate);

            var pagedList = await ApplyFilterAsync(query, filter);

            Guard.Against.NullNotFound(pagedList.Items, Resources.Torrent_NotFoundList_ErrorMessage);

            return pagedList;
        }

        public async Task<Torrent> FindAsync(int id)
        {
            Guard.Against.LessOne(id, Resources.Torrent_InvalidId_ErrorMessage);

            var torrent = await _torrentRepository.GetAsync(id);

            Guard.Against.NullNotFound(torrent, string.Format(Resources.Torrent_NotFoundById_ErrorMessage, id));

            return torrent;
        }

        public async Task<Torrent> AddAsync(Torrent torrent)
        {
            Guard.Against.NullInvalid(torrent, Resources.Torrent_Invalid_ErrorMessage);
            Guard.Against.NullString(torrent.Name, Resources.Torrent_InvalidName_ErrorMessage);
            Guard.Against.NullString(torrent.Description, Resources.Torrent_InvalidDescription_ErrorMessage);
            Guard.Against.NullString(torrent.Content, Resources.Torrent_InvalidContent_ErrorMessage);
            Guard.Against.LessOne(torrent.SubcategoryId, Resources.Torrent_InvalidSubcategoryId_ErrorMessage);

            if (!await _subcategoryRepository.ExistAsync(torrent.SubcategoryId))
            {
                throw new RutrackerException(
                    string.Format(Resources.Subcategory_NotFoundById_ErrorMessage, torrent.SubcategoryId),
                    ExceptionEventTypes.InvalidParameters);
            }

            torrent.TrackerId = null;
            torrent.Hash = null;
            torrent.Size = 0;
            torrent.IsStockTorrent = false;
            torrent.AddedDate = _dateService.Now();

            var result = await _torrentRepository.AddAsync(torrent);

            await _unitOfWork.SaveChangesAsync();

            var containerName = result.Id.ToString().PadLeft(10, '0');

            await _storageService.CreateContainerAsync(containerName);

            return result;
        }

        public async Task<Torrent> UpdateAsync(int id, Torrent torrent)
        {
            Guard.Against.NullInvalid(torrent, Resources.Torrent_Invalid_ErrorMessage);
            Guard.Against.NullInvalid(torrent.Name, Resources.Torrent_InvalidName_ErrorMessage);
            Guard.Against.NullInvalid(torrent.Description, Resources.Torrent_InvalidDescription_ErrorMessage);
            Guard.Against.NullString(torrent.Content, Resources.Torrent_InvalidContent_ErrorMessage);

            var result = await FindAsync(id);

            result.Name = torrent.Name;
            result.Description = torrent.Description;
            result.Content = torrent.Content;
            result.ModifiedDate = _dateService.Now();

            _torrentRepository.Update(result);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<Torrent> DeleteAsync(int id)
        {
            var torrent = await FindAsync(id);

            _torrentRepository.Delete(torrent);

            var containerName = torrent.Id.ToString().PadLeft(10, '0');

            await _storageService.DeleteContainerAsync(containerName);

            await _unitOfWork.SaveChangesAsync();

            return torrent;
        }

        public async Task<string> GetDownloadFileName(int id)
        {
            var torrent = await FindAsync(id);

            const int maxLength = 100;

            var fileName = torrent.Name.Length > maxLength
                ? torrent.Name.Substring(0, 100)
                : torrent.Name;

            return $"{fileName}.zip";
        }

        public async Task DownloadToStreamAsync(int id, Stream outStream)
        {
            Guard.Against.NullInvalid(outStream, "Invalid out stream.");

            var torrent = await FindAsync(id);

            if (torrent.IsStockTorrent)
            {
                throw new RutrackerException(
                    "Cannot download stock torrent.",
                    ExceptionEventTypes.InvalidParameters);
            }

            var containerName = torrent.Id.ToString().PadLeft(10, '0');

            await using var zipOutputStream = new ZipOutputStream(outStream);

            foreach (var file in torrent.Files)
            {
                zipOutputStream.SetLevel(0);
                zipOutputStream.PutNextEntry(new ZipEntry(file.Name));

                await _storageService.DownloadToStream(containerName, file.BlobName, zipOutputStream);
            }

            zipOutputStream.Finish();
            zipOutputStream.Close();
        }
    }
}