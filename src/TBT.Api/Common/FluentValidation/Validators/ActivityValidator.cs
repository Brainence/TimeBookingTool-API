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

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class ActivityValidator: ModelBaseValidator<ActivityModel>
    {
        public ActivityValidator(IActivityManager manager, ValidationMode mode):
            base(manager, mode)
        {

        }
    }
}