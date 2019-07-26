using System;

namespace Rutracker.Core.Exceptions
{
    public class TorrentException : Exception
    {
        public ExceptionEvent ExceptionEvent { get; }

        public TorrentException(ExceptionEvent exceptionEvent)
            : this(null, exceptionEvent)
        {
        }

        public TorrentException(string message, ExceptionEvent exceptionEvent)
            : base(message)
        {
            ExceptionEvent = exceptionEvent;
        }
    }
}