using System;

namespace Rutracker.Core.Exceptions
{
    public class TorrentException : Exception
    {
        public ExceptionEventType ExceptionEvent { get; }

        public TorrentException(ExceptionEventType exceptionEvent)
            : this(null, exceptionEvent)
        {
        }

        public TorrentException(string message, ExceptionEventType exceptionEvent)
            : base(message)
        {
            ExceptionEvent = exceptionEvent;
        }
    }
}