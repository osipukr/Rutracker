﻿using System.Linq;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class TorrentRepository : Repository<Torrent, int>, ITorrentRepository
    {
        public TorrentRepository(RutrackerContext context) : base(context)
        {
        }

        public IQueryable<Torrent> GetAll(string userId)
        {
            return GetAll(x => x.UserId == userId);
        }

        public IQueryable<Torrent> Search(string search, long? sizeFrom, long? sizeTo)
        {
            return GetAll(torrent =>
                (string.IsNullOrWhiteSpace(search) || torrent.Name.Contains(search)) &&
                (!sizeFrom.HasValue || torrent.Size >= sizeFrom) &&
                (!sizeTo.HasValue || torrent.Size <= sizeTo));
        }

        public IQueryable<Torrent> PopularTorrents(int count)
        {
            return _context.Comments.GroupBy(x => x.TorrentId,
                    (key, items) => new
                    {
                        Key = key,
                        Count = items.Count()
                    })
                .OrderByDescending(x => x.Count)
                .Take(count)
                .Join(_dbSet, group => group.Key, torrent => torrent.Id, (group, torrent) => torrent);
        }
    }
}