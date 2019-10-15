using System;
using System.Runtime.Serialization;

namespace Sharpy.ViewEngine.Exceptions
{
    [Serializable]
    public class RequiredParameterException : Exception
    {
        public RequiredParameterException()
        {
        }

        public RequiredParameterException(string message) : base(message)
        {
        }

        public RequiredParameterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequiredParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
