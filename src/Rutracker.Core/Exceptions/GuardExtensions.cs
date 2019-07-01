using System.Collections.Generic;
using Ardalis.GuardClauses;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Exceptions
{
    public static class TorrentGuards
    {
        public static void NullTorrent(this IGuardClause guardClause, long torrentId, Torrent torrent)
        {
            if (torrent == null)
            {
                throw new TorrentNotFoundException(torrentId);
            }
        }

        public static void NullTorrents(this IGuardClause guardClause, IEnumerable<Torrent> torrents)
        {
            if (torrents == null)
            {
                throw new TorrentNotFoundException("Torrent list empty");
            }
        }

        public static void NullTorrentForums(this IGuardClause guardClause, IEnumerable<(long Id, string Value, int Count)> forums)
        {
            if (forums == null)
            {
                throw new ForumNotFoundException("Torrent forum list empty");
            }
        }
    }
}