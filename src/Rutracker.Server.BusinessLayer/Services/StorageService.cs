using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Options;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class StorageService : IStorageService
    {
        private readonly CloudBlobClient _client;

        public StorageService(IOptions<StorageAuthOptions> storageOptions)
        {
            var storageSettings = storageOptions.Value;

            try
            {
                _client = CloudStorageAccount.Parse(storageSettings.ConnectionString).CreateCloudBlobClient();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public async Task<string> UploadFileFromStreamAsync(string containerName, string fileName, string fileType, Stream stream)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName, createIfNotExists: true);

            await blockBlob.UploadFromStreamAsync(stream);

            blockBlob.Properties.ContentType = fileType;
            await blockBlob.SetPropertiesAsync();

            return blockBlob.Uri.AbsoluteUri;
        }

        public async Task<string> UploadFileFromByteArrayAsync(string containerName, string fileName, string fileType, byte[] bytes)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName, createIfNotExists: true);

            await blockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);

            blockBlob.Properties.ContentType = fileType;
            await blockBlob.SetPropertiesAsync();

            return blockBlob.Uri.AbsoluteUri;
        }

        public async Task<Stream> DownloadFileToStreamAsync(string containerName, string fileName)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName);

            await using var stream = File.OpenWrite(fileName);
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
            var container = _client.GetContainerReference(containerName);

            return await container.DeleteIfExistsAsync();
        }

        public async Task<string> PathToFileAsync(string containerName, string fileName)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName);

            return blockBlob.Uri.AbsoluteUri;
        }

        private async Task<CloudBlockBlob> GetBlockBlobAsync(string containerName, string fileName, bool createIfNotExists = false)
        {
            var container = _client.GetContainerReference(containerName);

            if (createIfNotExists)
            {
                await container.CreateIfNotExistsAsync();
                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Container
                });
            }

            return container.GetBlockBlobReference(fileName);
        }
    }
}