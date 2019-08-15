using System;
using System.Runtime.Serialization;

namespace Rutracker.Shared.Infrastructure.Exceptions
{
    public class RutrackerException : Exception
    {
        protected RutrackerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public RutrackerException(ExceptionEventType exceptionEvent)
            : this(null, exceptionEvent)
        {
        }

        public RutrackerException(string message, ExceptionEventType exceptionEventType)
            : this(message, exceptionEventType, null)
        {
        }

        public RutrackerException(string message, ExceptionEventType exceptionEventType, Exception innerException)
            : base(message, innerException)
        {
            ExceptionEventType = exceptionEventType;
        }

        public ExceptionEventType ExceptionEventType { get; protected set; }
    }
}