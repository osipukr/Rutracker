using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ITorrentService
    {
        Task<Tuple<IEnumerable<Torrent>, int>> ListAsync(int page, int pageSize, int? categoryId, int? subcategoryId, string search);
        Task<IEnumerable<Torrent>> PopularAsync(int count);
        Task<Torrent> FindAsync(int id);
        Task<Torrent> FindAsync(int id, string userId);
        Task<Torrent> AddAsync(Torrent torrent);
        Task<Torrent> ChangeImageAsync(int id, string userId, string imageUrl);
        Task<Torrent> ChangeImageAsync(int id, string userId, string imageMimeType, Stream imageStream);
        Task<Torrent> DeleteImageAsync(int id, string userId);
        Task<Torrent> UpdateAsync(int id, string userId, Torrent torrent);
        Task<Torrent> DeleteAsync(int id, string userId);
    }
}