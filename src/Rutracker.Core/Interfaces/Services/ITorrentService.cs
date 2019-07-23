using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Interfaces.Services
{
    public interface ITorrentService
    {
        Task<IReadOnlyList<Torrent>> GetTorrentsOnPageAsync(int page, int pageSize,
            string search,
            IEnumerable<string> selectedTitleIds,
            long? sizeFrom,
            long? sizeTo);

        Task<Torrent> GetTorrentDetailsAsync(long id);

        Task<int> GetTorrentsCountAsync(string search,
            IEnumerable<string> selectedTitleIds,
            long? sizeFrom,
            long? sizeTo);

        Task<IReadOnlyList<(long Id, string Value, int Count)>> GetPopularForumsAsync(int count);
    }
}