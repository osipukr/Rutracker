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

        public RutrackerException(ExceptionEventTypes exceptionEventType)
            : this(null, exceptionEventType)
        {
        }

        public RutrackerException(string message, ExceptionEventTypes exceptionEventType)
            : this(message, exceptionEventType, null)
        {
        }

        public RutrackerException(string message, ExceptionEventTypes exceptionEventType, Exception innerException)
            : base(message, innerException)
        {
            ExceptionEventType = exceptionEventType;
        }

        public ExceptionEventTypes ExceptionEventType { get; protected set; }
    }
}