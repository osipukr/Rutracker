using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ITorrentService
    {
        Task<IEnumerable<Torrent>> ListAsync(int page, int pageSize, string search, IEnumerable<string> selectedForumIds, long? sizeFrom, long? sizeTo);
        Task<Torrent> FindAsync(long id);
        Task<int> CountAsync(string search, IEnumerable<string> selectedForumIds, long? sizeFrom, long? sizeTo);
        Task<IEnumerable<Tuple<long, string, int>>> ForumsAsync(int count);
    }
}