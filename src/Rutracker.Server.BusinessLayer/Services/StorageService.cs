using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Rutracker.Server.BusinessLayer.Interfaces;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class StorageService : IStorageService
    {
        private readonly CloudStorageAccount _account;

        public StorageService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Storage");

            CloudStorageAccount.TryParse(connectionString, out _account);
        }

        public async Task<bool> CreateContainerAsync(string containerName)
        {
            var container = _account.CreateCloudBlobClient().GetContainerReference(containerName);
            var result = await container.CreateIfNotExistsAsync();

            if (result)
            {
                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }

            return result;
        }

        public async Task<string> UploadFileAsync(string containerName, string fileName, string contentType, Stream inStream)
        {
            var container = _account.CreateCloudBlobClient().GetContainerReference(containerName);
            var block = container.GetBlockBlobReference(fileName);

            block.Properties.ContentType = contentType;

            await block.UploadFromStreamAsync(inStream);

            return block.Uri.AbsoluteUri;
        }

        public async Task<bool> DeleteContainerAsync(string containerName)
        {
            var container = _account.CreateCloudBlobClient().GetContainerReference(containerName);

            return await container.DeleteIfExistsAsync();
        }

        public async Task<bool> DeleteFileAsync(string containerName, string fileName)
        {
            var container = _account.CreateCloudBlobClient().GetContainerReference(containerName);
            var block = container.GetBlockBlobReference(fileName);

            return await block.DeleteIfExistsAsync();
        }

        public async Task DownloadToStream(string containerName, string fileName, Stream outStream)
        {
            var container = _account.CreateCloudBlobClient().GetContainerReference(containerName);
            var block = container.GetBlockBlobReference(fileName);

            await block.DownloadToStreamAsync(outStream);
        }
    }
}