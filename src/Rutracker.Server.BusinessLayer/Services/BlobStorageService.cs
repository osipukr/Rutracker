using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Rutracker.Server.BusinessLayer.Exceptions;
using Rutracker.Server.BusinessLayer.Interfaces;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class BlobStorageService : IStorageService
    {
        private readonly CloudBlobClient _client;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("StorageConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _client = CloudStorageAccount.Parse(connectionString).CreateCloudBlobClient();

            if (_client == null)
            {
                throw new TorrentException($"", ExceptionEventType.NotValidParameters);
            }
        }

        public async Task UploadFileAsync(string containerName, string fileName, Stream stream)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName, createIfNotExists: true);

            await blockBlob.UploadFromStreamAsync(stream);
        }

        public async Task<Stream> DownloadFileAsync(string containerName, string fileName)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName);

            using var stream = File.OpenWrite(fileName);

            await blockBlob.DownloadToStreamAsync(stream);

            return stream;
        }

        public async Task<bool> DeleteFileAsync(string containerName, string fileName)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName);

            return await blockBlob.DeleteIfExistsAsync();
        }

        public async Task<string> PathToFileAsync(string containerName, string fileName)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName);

            return blockBlob.Uri.AbsoluteUri;
        }

        private async Task<CloudBlockBlob> GetBlockBlobAsync(string containerName, string fileName, bool createIfNotExists = false)
        {
            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new TorrentException($"The {nameof(containerName)} not valid.", ExceptionEventType.NotValidParameters);
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new TorrentException($"The {nameof(fileName)} not valid.", ExceptionEventType.NotValidParameters);
            }

            var container = _client.GetContainerReference(containerName);

            if (createIfNotExists && !await container.CreateIfNotExistsAsync())
            {
                throw new TorrentException($"Failed to create container {containerName}.", ExceptionEventType.NotValidParameters);
            }

            var blockBlob = container.GetBlockBlobReference(fileName);

            if (blockBlob == null)
            {
                throw new TorrentException($"The {nameof(blockBlob)} not found.", ExceptionEventType.NotFound);
            }

            return blockBlob;
        }
    }
}