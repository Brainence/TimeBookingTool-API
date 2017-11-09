using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBT.Api.Common.FluentValidation.Interfaces
{
    interface IValidatorFactory
    {
        T Create<T>(string validatorName);
    }
}
