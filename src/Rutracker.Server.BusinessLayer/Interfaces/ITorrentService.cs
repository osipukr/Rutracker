using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.BusinessLayer.Interfaces
{
    public interface ITorrentService
    {
        Task<Tuple<IEnumerable<Torrent>, int>> ListAsync(int page, int pageSize, int? categoryId, int? subcategoryId, string search);
        Task<IEnumerable<Torrent>> PopularTorrentsAsync(int count);
        Task<Torrent> FindAsync(int id);
    }
}