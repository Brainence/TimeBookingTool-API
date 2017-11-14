using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBT.Business.Models.BusinessModels;
using TBT.Api.Common.FluentValidation.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Api.Common.Filters.Base;

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class TimeEntryValidator: ModelValidatorBase<TimeEntryModel>
    {
        public TimeEntryValidator(ITimeEntryManager manager, ValidationMode mode) :
            base(manager, mode)
        {

        }
    }
}