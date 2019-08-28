using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeKicker.BBCode;
using Microsoft.EntityFrameworkCore;
using Rutracker.Server.BusinessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Shared.Infrastructure.Exceptions;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly ITorrentRepository _torrentRepository;

        public TorrentService(ITorrentRepository torrentRepository)
        {
            _torrentRepository = torrentRepository;
        }

        public async Task<IEnumerable<Torrent>> ListAsync(int page, int pageSize, string search, IEnumerable<string> selectedForumIds, long? sizeFrom, long? sizeTo)
        {
            if (page < 1)
            {
                throw new RutrackerException($"The {nameof(page)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            if (pageSize < 1 || pageSize > 100)
            {
                throw new RutrackerException($"The {nameof(pageSize)} is out of range (1 - 100).", ExceptionEventType.NotValidParameters);
            }

            var forumIds = ConvertForumIds(selectedForumIds);
            var torrents = await _torrentRepository.Search(search, forumIds, sizeFrom, sizeTo)
                .OrderBy(torrent => torrent.RegisteredAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (torrents == null)
            {
                throw new RutrackerException("The torrents not found.", ExceptionEventType.NotFound);
            }

            return torrents;
        }

        public async Task<Torrent> FindAsync(int id)
        {
            if (id < 1)
            {
                throw new RutrackerException($"The {nameof(id)} is less than 1.", ExceptionEventType.NotValidParameters);
            }

            var torrent = await _torrentRepository.GetAsync(id);

            if (torrent == null)
            {
                throw new RutrackerException("The torrent not found.", ExceptionEventType.NotFound);
            }

            if (!string.IsNullOrWhiteSpace(torrent.Content))
            {
                torrent.Content = BBCode.ToHtml(torrent.Content);
            }

            return torrent;
        }

        public async Task<int> CountAsync(string search, IEnumerable<string> selectedForumIds, long? sizeFrom, long? sizeTo)
        {
            var forumIds = ConvertForumIds(selectedForumIds);

            return await _torrentRepository.Search(search, forumIds, sizeFrom, sizeTo).CountAsync();
        }

        public async Task<IEnumerable<Tuple<long, string, int>>> ForumsAsync(int count)
        {
            if (count < 1 || count > 100)
            {
                throw new RutrackerException($"The {nameof(count)} is out of range (1 - 100).", ExceptionEventType.NotValidParameters);
            }

            var forums = await _torrentRepository.GetForums(count).ToListAsync();

            if (forums == null)
            {
                throw new RutrackerException("The forums not found.", ExceptionEventType.NotFound);
            }

            return forums;
        }

        private static long[] ConvertForumIds(IEnumerable<string> selectedForumIds) =>
            selectedForumIds?.Select(x => new
            {
                Success = long.TryParse(x, out var value),
                Value = value
            })
            .Where(x => x.Success)
            .Select(x => x.Value)
            .ToArray();
    }
}