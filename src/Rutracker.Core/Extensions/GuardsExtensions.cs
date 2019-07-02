using System.Collections.Generic;
using Ardalis.GuardClauses;
using Rutracker.Core.Entities;
using Rutracker.Core.Exceptions;

namespace Rutracker.Core.Extensions
{
    public static class GuardsExtensions
    {
        public static void NullTorrent(this IGuardClause guardClause, long torrentId, Torrent torrent)
        {
            if (torrent == null)
            {
                throw new GenericException($"Torrent with id {torrentId} not found.", ExceptionEvent.NotFound);
            }
        }

        public static void NullTorrents(this IGuardClause guardClause, IEnumerable<Torrent> torrents)
        {
            if (torrents == null)
            {
                throw new GenericException("Torrent list empty.", ExceptionEvent.NotFound);
            }
            
        }

        public static void NullTorrentForums(this IGuardClause guardClause, IEnumerable<(long Id, string Value, int Count)> forums)
        {
            if (forums == null)
            {
                throw new GenericException("Forum list empty.", ExceptionEvent.NotFound);
            }
        }

        public static void OutOfRange(this IGuardClause guardClause, string param, long input, long rangeFrom, long rangeTo)
        {
            if (input < rangeFrom || input > rangeTo)
            {
                throw new GenericException($"The {param} is out of range.", ExceptionEvent.NotValidParameters);
            }
        }
    }
}