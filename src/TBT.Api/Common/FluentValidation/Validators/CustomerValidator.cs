using FluentValidation;
using TBT.Business.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class CustomerValidator: AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Id).GreaterThan(0);
            RuleFor(customer => customer.IsActive).NotEqual(false);
        }
    }
}