using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using TBT.Business.Models.BusinessModels;
using TBT.Api.Common.FluentValidation.Base;
using TBT.Business.Interfaces;
using TBT.Api.Common.Filters.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Infrastructure.CastleWindsor;

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class TimeEntryValidator: ModelValidatorBase<TimeEntryModel>
    {
        public TimeEntryValidator(ITimeEntryManager manager, ValidationMode mode) :
            base(manager, mode)
        {
            RuleFor(timeEntry => timeEntry.User)
                .MustAsync(async (x, token) => x.Id > 0 && await ExistsAsync(x.Id, ServiceLocator.Current.Get<IUserManager>()))
                .When(x => HasFlag(ValidationMode.Add | ValidationMode.Update))
                .WithMessage("{PropertyName} doesn't exist.");
            RuleFor(timeEntry => timeEntry.Activity)
                .MustAsync(async (x, token) => x.Id > 0 && await ExistsAsync(x.Id, ServiceLocator.Current.Get<IActivityManager>()))
                .When(x => HasFlag(ValidationMode.Add | ValidationMode.Update))
                .WithMessage("{PropertyName} doesn't exist.");
            RuleFor(timeEntry => timeEntry.IsActive)
                .Equal(true)
                .When(x => HasFlag(ValidationMode.Add))
                .WithMessage("{PropertyName} can't be {PropertyValue}.");
        }
    }
}