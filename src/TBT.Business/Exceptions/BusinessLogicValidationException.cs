using System;

namespace TBT.Business.Exceptions
{
    public class BusinessLogicValidationException : Exception
    {
        public BusinessLogicValidationException()
            : base()
        { }

        public BusinessLogicValidationException(string message)
            : base(message)
        { }

        public BusinessLogicValidationException(string messageToFormat, params object[] parameters)
            : base(string.Format(messageToFormat, parameters))
        { }

        public BusinessLogicValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
