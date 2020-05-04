using System.IO;
using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IStorageService
    {
        Task<bool> CreateContainerAsync(string containerName);
        Task<string> UploadFileAsync(string containerName, string fileName, string contentType, Stream inStream);
        Task<bool> DeleteContainerAsync(string containerName);
        Task<bool> DeleteFileAsync(string containerName, string fileName);
        Task DownloadToStream(string containerName, string fileName, Stream outStream);
    }
}