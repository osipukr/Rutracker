using System;

namespace Rutracker.Server.BusinessLayer.Exceptions
{
    public class TorrentException : Exception
    {
        public ExceptionEventType ExceptionEventType { get; private set; }

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