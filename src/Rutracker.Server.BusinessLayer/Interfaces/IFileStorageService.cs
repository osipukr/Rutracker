using System.IO;
using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IFileStorageService
    {
        Task CreateImagesContainerAsync();
        Task CreateTorrentContainerAsync(int torrentId);
        Task<string> UploadUserImageAsync(string userId, string mimeType, Stream imageStream);
        Task<string> UploadTorrentImageAsync(int torrentId, string mimeType, Stream imageStream);
        Task<string> UploadTorrentFileAsync(int torrentId, string mimeType, string name, Stream fileStream);
        Task DeleteUserImageAsync(string userId);
        Task DeleteTorrentImageAsync(int torrentId);
        Task DeleteTorrentFileAsync(int torrentId, string name);
        Task DeleteTorrentFilesAsync(int torrentId);
    }
}