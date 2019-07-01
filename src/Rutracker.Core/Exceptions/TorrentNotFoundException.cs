using System;
using System.Runtime.Serialization;

namespace Rutracker.Core.Exceptions
{
    public class TorrentNotFoundException : Exception
    {
        protected TorrentNotFoundException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public TorrentNotFoundException(long torrentId)
            : base($"No torrent found with id {torrentId}")
        {
        }

        public TorrentNotFoundException(string message) 
            : base(message)
        {
        }

        public TorrentNotFoundException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}