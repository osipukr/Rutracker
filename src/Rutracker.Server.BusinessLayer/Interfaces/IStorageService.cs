using System.IO;
using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IStorageService
    {
        Task<bool> CreateContainerAsync(string containerName, bool isPrivate);
        Task<string> UploadFileAsync(string containerName, string fileName, Stream stream);
        Task<bool> DeleteFileAsync(string containerName, string fileName);
        Task<bool> DeleteContainerAsync(string containerName);
    }
}