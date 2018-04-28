using System;
using System.Runtime.Serialization;

namespace DontForgetTheEggs.Core.Common.Exceptions
{
    public class EggsDataException : Exception
    {
        public EggsDataException()
        {
        }

        public EggsDataException(string message) : base(message)
        {
        }

        public EggsDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EggsDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
