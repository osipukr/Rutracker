using System;
using System.IO;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Options;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class StorageService : IStorageService
    {
        private readonly StorageSettings _storageSettings;
        private readonly CloudBlobClient _client;

        public StorageService(IOptions<StorageSettings> storageOptions)
        {
            _storageSettings = storageOptions.Value;

            try
            {
                _client = CloudStorageAccount.Parse(_storageSettings.ConnectionString).CreateCloudBlobClient();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public async Task<string> UploadFileAsync(string containerName, string fileName, Stream stream)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName, createIfNotExists: true);

            await blockBlob.UploadFromStreamAsync(stream);

            return blockBlob.Uri.AbsoluteUri;
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

        public async Task<bool> DeleteContainerAsync(string containerName)
        {
            var container = GetBlobContainer(containerName);

            return await container.DeleteIfExistsAsync();
        }

        public async Task<string> PathToFileAsync(string containerName, string fileName)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName);

            return blockBlob.Uri.AbsoluteUri;
        }

        private CloudBlobContainer GetBlobContainer(string containerName)
        {
            Guard.Against.NullOrWhiteSpace(containerName, message: "The container name not valid.");

            return _client.GetContainerReference(containerName);
        }

        private async Task<CloudBlockBlob> GetBlockBlobAsync(string containerName, string fileName, bool createIfNotExists = false)
        {
            var container = GetBlobContainer(containerName);

            if (createIfNotExists)
            {
                await container.CreateIfNotExistsAsync();
                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Container
                });
            }

            Guard.Against.NullOrWhiteSpace(fileName, message: "The file name not valid.");

            var blockBlob = container.GetBlockBlobReference(fileName);

            Guard.Against.NullNotFound(blockBlob, "The block blob not found.");

            return blockBlob;
        }
    }
}