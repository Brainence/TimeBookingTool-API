using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBT.Api.Common.FluentValidation.Validators;
using FluentValidation;
using TBT.Api.Common.Filters.Base;
using TBT.Business.Interfaces;
using TBT.Api.Common.FluentValidation.Base;
using TBT.Api.Common.FluentValidation.Interfaces;

namespace TBT.Api.Common.FluentValidation.Store.Interfaces
{
    public interface IValidatorStore
    {
        IModelValidatorBase GetValidator(ValidationMode mode, Type model);
    }
}
