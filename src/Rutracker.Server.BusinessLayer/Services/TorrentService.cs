using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CodeKicker.BBCode;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly ITorrentRepository _torrentRepository;

        public TorrentService(ITorrentRepository torrentRepository)
        {
            _torrentRepository = torrentRepository;
        }

        public async Task<IEnumerable<Torrent>> ListAsync(int page, int pageSize, int? categoryId, int? subcategoryId, string search)
        {
            Guard.Against.LessOne(page, $"The {nameof(page)} is less than 1.");
            Guard.Against.OutOfRange(pageSize, rangeFrom: 1, rangeTo: 100, $"The {nameof(pageSize)} is out of range (1 - 100).");

            if (categoryId.HasValue)
            {
                Guard.Against.LessOne(categoryId.Value, $"The {nameof(subcategoryId)} is less than 1.");
            }

            if (subcategoryId.HasValue)
            {
                Guard.Against.LessOne(subcategoryId.Value, $"The {nameof(subcategoryId)} is less than 1.");
            }

            var torrents = await _torrentRepository.Search(categoryId, subcategoryId, search)
                .OrderBy(torrent => torrent.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Guard.Against.NullNotFound(torrents, "The torrents not found.");

            return torrents;
        }

        public async Task<IEnumerable<Torrent>> PopularTorrentsAsync(int count)
        {
            Guard.Against.OutOfRange(count, rangeFrom: 1, rangeTo: 100, $"The {nameof(count)} is out of range (1 - 100).");

            var torrents = await _torrentRepository.PopularTorrents(count).ToListAsync();

            Guard.Against.NullNotFound(torrents, "The torrents not found.");

            return torrents;
        }

        public async Task<Torrent> FindAsync(int id)
        {
            Guard.Against.LessOne(id, $"The {nameof(id)} is less than 1.");

            var torrent = await _torrentRepository.GetAsync(id);

            Guard.Against.NullNotFound(torrent, "The torrent not found.");

            if (!string.IsNullOrWhiteSpace(torrent.Content))
            {
                torrent.Content = BBCode.ToHtml(torrent.Content);
            }

            return torrent;
        }

        public async Task<int> CountAsync(int? categoryId, int? subcategoryId, string search)
        {
            return await _torrentRepository.Search(categoryId, subcategoryId, search).CountAsync();
        }
    }
}