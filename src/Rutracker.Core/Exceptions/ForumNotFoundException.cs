using System;
using System.Runtime.Serialization;

namespace Rutracker.Core.Exceptions
{
    public class ForumNotFoundException : Exception
    {
        protected ForumNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ForumNotFoundException(string message)
            : base(message)
        {
        }

        public ForumNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}