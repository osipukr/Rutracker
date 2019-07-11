using System;
using System.Runtime.Serialization;

namespace Rutracker.Core.Exceptions
{
    public class TorrentException : Exception
    {
        public ExceptionEvent ExceptionEvent { get; private set; }

        protected TorrentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public TorrentException(ExceptionEvent exceptionEvent)
            : this(null, exceptionEvent)
        {
        }

        public TorrentException(string message, ExceptionEvent exceptionEvent, Exception innerException = null)
            : base(message, innerException)
        {
            ExceptionEvent = exceptionEvent;
        }
    }
}