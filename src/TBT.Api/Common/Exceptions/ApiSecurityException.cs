using System;

namespace TBT.WebApi.Exceptions
{
    public class ApiSecurityException: Exception
    {
        public ApiSecurityException()
            : base()
        { }

        public ApiSecurityException(string message)
            : base(message)
        { }

        public ApiSecurityException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
