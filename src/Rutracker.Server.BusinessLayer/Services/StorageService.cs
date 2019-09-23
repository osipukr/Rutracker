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
        private readonly CloudStorageAccount _account;
        private readonly StorageAuthOptions _storageAuthOptions;

        public StorageService(IOptions<StorageAuthOptions> storageOptions)
        {
            _storageAuthOptions = storageOptions.Value;

            CloudStorageAccount.TryParse(_storageAuthOptions.ConnectionString, out _account);
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

        public async Task<string> UploadFileAsync(string containerName, string fileName, Stream stream)
        {
            var container = _account.CreateCloudBlobClient().GetContainerReference(containerName);
            var block = container.GetBlockBlobReference(fileName);

            await block.UploadFromStreamAsync(stream);

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
    }
}