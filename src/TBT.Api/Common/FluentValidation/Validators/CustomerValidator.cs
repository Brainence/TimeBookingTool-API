using FluentValidation;
using TBT.Business.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBT.Business.Interfaces;
using TBT.Api.Common.Filters.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Api.Common.FluentValidation.Base;

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class CustomerValidator: ModelValidatorBase<CustomerModel>
    {
        public CustomerValidator(ICustomerManager manager, ValidationMode mode) :
            base(manager, mode)
        {

        }
    }
}