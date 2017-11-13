using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBT.Api.Common.Filters.Base;

namespace TBT.Api.Common.FluentValidation.Interfaces
{
    interface IValidatorFactory
    {
        T Create<T>(ValidationMode mode, Type modelType);
    }
}
