using System;
using System.Runtime.Serialization;

namespace Mmu.Mlh.RestExtensionsSimple.Areas.Exceptions
{
    [Serializable]
    public class HttpCallException : Exception
    {
        public HttpCallException()
        {
        }

        public HttpCallException(string message)
            : base(message)
        {
        }

        public HttpCallException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected HttpCallException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}