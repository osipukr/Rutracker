using System.IO;
using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(string containerName, string fileName, Stream stream);
        Task<Stream> DownloadFileToStreamAsync(string containerName, string fileName);
        Task<bool> DeleteFileAsync(string containerName, string fileName);
        Task<bool> DeleteContainerAsync(string containerName);
        Task<string> PathToFileAsync(string containerName, string fileName);
    }
}