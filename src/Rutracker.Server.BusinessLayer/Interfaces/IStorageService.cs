using System.IO;
using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadFileFromStreamAsync(string containerName, string fileName, string fileType, Stream stream);
        Task<string> UploadFileFromByteArrayAsync(string containerName, string fileName, string fileType, byte[] bytes);
        Task<Stream> DownloadFileToStreamAsync(string containerName, string fileName);
        Task<bool> DeleteFileAsync(string containerName, string fileName);
        Task<bool> DeleteContainerAsync(string containerName);
        Task<string> PathToFileAsync(string containerName, string fileName);
    }
}