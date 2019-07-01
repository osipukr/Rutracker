using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Rutracker.Core.Entities;
using Rutracker.Core.Exceptions;
using Rutracker.Core.Interfaces;
using Rutracker.Core.Specifications;

namespace Rutracker.Core.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly ITorrentRepository _torrentRepository;

        public TorrentService(ITorrentRepository torrentRepository) => _torrentRepository = torrentRepository;

        public async Task<IReadOnlyList<Torrent>> GetTorrentsOnPageAsync(int page, int pageSize, string search,
            IEnumerable<string> selectedTitleIds, long? sizeFrom, long? sizeTo)
        {
            var specification = new TorrentsFilterPaginatedSpecification((page - 1) * pageSize, pageSize, search,
                selectedTitleIds, sizeFrom, sizeTo);

            var torrents = await _torrentRepository.ListAsync(specification);

            Guard.Against.NullTorrents(torrents);

            return torrents;
        }

        public async Task<Torrent> GetTorrentDetailsAsync(long id)
        {
            var specification = new TorrentWithForumAndFilesSpecification(id);

            var torrent = await _torrentRepository.GetAsync(specification);

            Guard.Against.NullTorrent(id, torrent);

            return torrent;
        }

        public async Task<int> GetTorrentsCountAsync(string search,
            IEnumerable<string> selectedTitleIds,
            long? sizeFrom,
            long? sizeTo)
        {
            var specification = new TorrentsFilterSpecification(search, selectedTitleIds, sizeFrom, sizeTo);

            var count = await _torrentRepository.CountAsync(specification);

            return count;
        }

        public async Task<IReadOnlyList<(long Id, string Value, int Count)>> GetPopularForumsAsync(int count)
        {
            var forums = await _torrentRepository.GetPopularForumsAsync(count);

            Guard.Against.NullTorrentForums(forums);

            return forums;
        }
    }
}