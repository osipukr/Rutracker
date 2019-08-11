using System;

namespace Rutracker.Core.Exceptions
{
    public class TorrentException : Exception
    {
        public ExceptionEventType ExceptionEventType { get; }

        public TorrentException(ExceptionEventType exceptionEvent)
            : this(null, exceptionEvent)
        {
        }

        public TorrentException(string message, ExceptionEventType exceptionEventType)
            : base(message)
        {
            ExceptionEventType = exceptionEventType;
        }
    }
}