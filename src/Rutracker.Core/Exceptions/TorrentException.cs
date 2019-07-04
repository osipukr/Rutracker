using System;
using System.Runtime.Serialization;

namespace Rutracker.Core.Exceptions
{
    public class TorrentException : Exception
    {
        public ExceptionEvent ExceptionEvent { get; }

        protected TorrentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public TorrentException(ExceptionEvent exceptionEvent)
            : this(null, exceptionEvent, null)
        {
        }

        public TorrentException(string message, ExceptionEvent exceptionEvent)
            : this(message, exceptionEvent, null)
        {
        }

        public TorrentException(string message, ExceptionEvent exceptionEvent, Exception innerException)
            : base(message, innerException)
        {
            ExceptionEvent = exceptionEvent;
        }
    }
}