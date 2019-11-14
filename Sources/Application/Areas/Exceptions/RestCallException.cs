using System;
using System.Runtime.Serialization;

namespace Mmu.Mlh.RestExtensions.Areas.Exceptions
{
    [Serializable]
    public class RestCallException : Exception
    {
        public RestCallException()
        {
        }

        public RestCallException(string message)
            : base(message)
        {
        }

        public RestCallException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RestCallException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}