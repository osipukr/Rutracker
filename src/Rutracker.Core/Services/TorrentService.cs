﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Rutracker.Core.Entities;
using Rutracker.Core.Extensions;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Specifications;

namespace Rutracker.Core.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly ITorrentRepository _torrentRepository;

        public TorrentService(ITorrentRepository torrentRepository) => _torrentRepository = torrentRepository;

        public async Task<IReadOnlyList<Torrent>> GetTorrentsOnPageAsync(int page,
            int pageSize,
            string search,
            IEnumerable<string> selectedTitleIds,
            long? sizeFrom,
            long? sizeTo)
        {
            Guard.Against.OutOfRange(nameof(page), page, 1, int.MaxValue);
            Guard.Against.OutOfRange(nameof(pageSize) ,pageSize, 1, int.MaxValue);

            var specification = new TorrentsFilterPaginatedSpecification((page - 1) * pageSize, pageSize, search,
                selectedTitleIds, sizeFrom, sizeTo);

            var torrents = await _torrentRepository.ListAsync(specification);

            Guard.Against.Null(nameof(torrents), torrents);

            return torrents;
        }

        public async Task<Torrent> GetTorrentDetailsAsync(long id)
        {
            Guard.Against.OutOfRange(nameof(id), id, 1, long.MaxValue);

            var specification = new TorrentWithForumAndFilesSpecification(id);
            var torrent = await _torrentRepository.GetAsync(specification);

            Guard.Against.Null(nameof(torrent), torrent);

            return torrent;
        }

        public async Task<int> GetTorrentsCountAsync(string search,
            IEnumerable<string> selectedTitleIds,
            long? sizeFrom,
            long? sizeTo)
        {
            var specification = new TorrentsFilterSpecification(search, selectedTitleIds, sizeFrom, sizeTo);
            var count = await _torrentRepository.CountAsync(specification);

            Guard.Against.OutOfRange(nameof(count), count, 0, int.MaxValue);

            return count;
        }

        public async Task<IReadOnlyList<(long Id, string Value, int Count)>> GetPopularForumsAsync(int count)
        {
            Guard.Against.OutOfRange(nameof(count), count, 1, int.MaxValue);

            var forums = await _torrentRepository.GetPopularForumsAsync(count);

            Guard.Against.Null(nameof(forums), forums);

            return forums;
        }
    }
}