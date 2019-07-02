using System;
using System.Runtime.Serialization;

namespace Rutracker.Core.Exceptions
{
    public class GenericException : Exception
    {
        public ExceptionEvent ExceptionEvent { get; }

        protected GenericException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public GenericException(string message, ExceptionEvent exceptionEvent)
            : this(message, exceptionEvent, null)
        {
        }

        public GenericException(string message, ExceptionEvent exceptionEvent, Exception innerException)
            : base(message, innerException)
        {
            ExceptionEvent = exceptionEvent;
        }
    }
}