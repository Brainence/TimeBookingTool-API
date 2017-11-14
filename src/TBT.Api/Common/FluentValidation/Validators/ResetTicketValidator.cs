using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using TBT.Business.Models.BusinessModels;
using TBT.Api.Common.FluentValidation.Base;
using TBT.Api.Common.Filters.Base;
using TBT.Business.Managers.Interfaces;

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class ResetTicketValidator: ModelValidatorBase<ResetTicketModel>
    {
        public ResetTicketValidator(IResetTicketManager manager, ValidationMode mode) :
            base(manager, mode)
        {

        }
    }
}