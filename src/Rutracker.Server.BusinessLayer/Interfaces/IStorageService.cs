using System.IO;
using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IStorageService
    {
        Task<bool> CreateContainerAsync(string containerName);
        Task<string> UploadFileAsync(string containerName, string fileName, Stream stream);
        Task<bool> DeleteContainerAsync(string containerName);
        Task<bool> DeleteFileAsync(string containerName, string fileName);
    }
}