using System;
using System.Runtime.Serialization;

namespace ASmarty.ViewEngine.Exceptions
{
    [Serializable]
    public class RequiredAttributeException : Exception
    {
        public RequiredAttributeException()
        {
        }

        public RequiredAttributeException(string message) : base(message)
        {
        }

        public RequiredAttributeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequiredAttributeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}