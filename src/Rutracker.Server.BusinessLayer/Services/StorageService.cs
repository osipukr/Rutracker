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
        private readonly StorageAuthOptions _storageAuthOptions;

        public StorageService(IOptions<StorageAuthOptions> storageOptions)
        {
            _storageAuthOptions = storageOptions.Value;
        }

        public async Task<string> UploadFileAsync(string containerName, string fileName, Stream stream)
        {
            var blockBlob = await GetBlockBlobAsync(containerName, fileName, createIfNotExists: true);

            await blockBlob.UploadFromStreamAsync(stream);

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
            var client = CloudStorageAccount.Parse(_storageAuthOptions.ConnectionString).CreateCloudBlobClient();

            return client.GetContainerReference(containerName);
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

            return container.GetBlockBlobReference(fileName);
        }
    }
}