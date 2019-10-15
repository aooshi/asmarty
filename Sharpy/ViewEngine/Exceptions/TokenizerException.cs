using System;
using System.Runtime.Serialization;

namespace Sharpy.ViewEngine.Exceptions
{
    [Serializable]
    public class TokenizerException : Exception
    {
        public TokenizerException()
        {
        }

        public TokenizerException(string message) : base(message)
        {
        }

        public TokenizerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TokenizerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}