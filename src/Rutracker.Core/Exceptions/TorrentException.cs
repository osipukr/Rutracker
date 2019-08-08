using System;

namespace Rutracker.Core.Exceptions
{
    public class TorrentException : Exception
    {
        public ExceptionEventType ExceptionType { get; }

        public TorrentException(ExceptionEventType exceptionEvent)
            : this(null, exceptionEvent)
        {
        }

        public TorrentException(string message, ExceptionEventType exceptionType)
            : base(message)
        {
            ExceptionType = exceptionType;
        }
    }
}