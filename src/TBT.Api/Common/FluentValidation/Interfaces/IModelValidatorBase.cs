using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using System.Threading.Tasks;
using TBT.Api.Common.Filters.Base;
using FluentValidation.Results;

namespace TBT.Api.Common.FluentValidation.Interfaces
{
    public interface IModelValidatorBase
    {
        Task<ValidationResult> ValidateAsync(object value);
        ValidationMode Mode { get; }
    }
}