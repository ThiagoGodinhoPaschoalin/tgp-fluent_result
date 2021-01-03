using System;
using System.Runtime.Serialization;

namespace Tgp.FluentResult.Core.Exceptions
{
    [Serializable]
    public class FluentResultException : Exception
    {
        public FluentResultException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public FluentResultException(string className, string methodName, string propertyName, string message) 
            : base($"FluentResult: [ {className}.{methodName} ] [ {propertyName}: {message} ]")
        { }

        protected FluentResultException()
        { }

        protected FluentResultException(string message)
            : base(message)
        { }

        protected FluentResultException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        { }
    }
}