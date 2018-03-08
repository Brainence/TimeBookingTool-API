using System;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.FluentValidation.Interfaces;

namespace TBT.Api.Common.FluentValidation.Store.Interfaces
{
    public interface IValidatorStore
    {
        IModelValidatorBase GetValidator(ValidationMode mode, Type model);
    }
}
