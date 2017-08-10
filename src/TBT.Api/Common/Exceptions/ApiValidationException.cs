using System;

namespace TBT.WebApi.Exceptions
{
    public class ApiValidationException : Exception
    {
        public ApiValidationException()
            : base()
        { }

        public ApiValidationException(string message)
            : base(message)
        { }

        public ApiValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
