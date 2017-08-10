using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBT.Business.Exceptions
{
    public class SecurityException : Exception
    {
        public SecurityException()
            : base()
        { }

        public SecurityException(string message)
            : base(message)
        { }

        public SecurityException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
