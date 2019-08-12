using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Rutracker.Server.BusinessLayer.Extensions;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.BusinessLayer.Specifications;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly ITorrentRepository _torrentRepository;

        public TorrentService(ITorrentRepository torrentRepository)
        {
            _torrentRepository = torrentRepository ?? throw new ArgumentNullException(nameof(torrentRepository));
        }

        public async Task<IReadOnlyList<Torrent>> GetTorrentsOnPageAsync(int page, int pageSize,
            string search,
            IEnumerable<string> selectedTitleIds,
            long? sizeFrom,
            long? sizeTo)
        {
            Guard.Against.OutOfRange(nameof(page), page, rangeFrom: 1, rangeTo: int.MaxValue);
            Guard.Against.OutOfRange(nameof(pageSize), pageSize, rangeFrom: 1, rangeTo: 100);

            var specification = new TorrentsFilterPaginatedSpecification(
                skip: (page - 1) * pageSize,
                take: pageSize,
                search,
                selectedTitleIds,
                sizeFrom,
                sizeTo);

            var torrents = await _torrentRepository.ListAsync(specification);

            Guard.Against.Null(nameof(torrents), torrents);

            return torrents;
        }

        public async Task<Torrent> GetTorrentDetailsAsync(long id)
        {
            Guard.Against.OutOfRange(nameof(id), id, rangeFrom: 1, rangeTo: long.MaxValue);

            var specification = new TorrentWithForumAndFilesSpecification(id);
            var torrent = await _torrentRepository.GetAsync(specification);

            Guard.Against.Null(nameof(torrent), torrent);

            return torrent;
        }

        public async Task<int> GetTorrentsCountAsync(string search, IEnumerable<string> selectedTitleIds, long? sizeFrom, long? sizeTo)
        {
            var specification = new TorrentsFilterSpecification(search, selectedTitleIds, sizeFrom, sizeTo);
            var count = await _torrentRepository.CountAsync(specification);

            Guard.Against.OutOfRange(nameof(count), count, rangeFrom: 0, rangeTo: int.MaxValue);

            return count;
        }

        public async Task<IReadOnlyList<(long Id, string Value, int Count)>> GetPopularForumsAsync(int count)
        {
            Guard.Against.OutOfRange(nameof(count), count, rangeFrom: 1, rangeTo: 100);

            var forums = await _torrentRepository.GetPopularForumsAsync(count);

            Guard.Against.Null(nameof(forums), forums);

            return forums;
        }
    }
}