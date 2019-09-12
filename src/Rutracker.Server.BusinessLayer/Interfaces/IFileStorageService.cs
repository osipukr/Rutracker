using System.IO;
using System.Threading.Tasks;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadUserImageAsync(string userId, Stream imageStream);
        Task<string> UploadTorrentImageAsync(int torrentId, Stream imageStream);
        Task<string> UploadTorrentFileAsync(int torrentId, Stream fileStream);
        Task DeleteUserImageAsync(string userId);
    }
}