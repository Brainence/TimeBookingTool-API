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
    public class ActivityValidator: ModelValidatorBase<ActivityModel>
    {
        public ActivityValidator(IActivityManager manager, ValidationMode mode):
            base(manager, mode)
        {
            //RuleFor(activity => activity.Project).Must(x => x.Id > 0)
            //    .When(x => HasFlag(ValidationMode.Exist))
            //    .WithMessage("ProjectId can't be less or equal 0.");
            RuleFor(activity => activity.Name).NotEmpty()
                .When(x => HasFlag(ValidationMode.Add | ValidationMode.Update | ValidationMode.DataRelevance))
                .WithMessage("{PropertyName} can't be empty.");
            RuleFor(activity => activity.IsActive).Equal(true)
                .When(x => HasFlag(ValidationMode.Add))
                .WithMessage("{PropertyName} can't be {PropertyValue}.");
            RuleFor(activity => activity.Project).NotNull()
                .MustAsync(async (x, token) => await ExistsAsync(x.Id, ServiceLocator.Current.Get<IProjectManager>()))
                .When(x => HasFlag(ValidationMode.Add | ValidationMode.Update))
                .WithMessage("{PropertyName} can't be null or doesn't exists.");
        }
    }
}