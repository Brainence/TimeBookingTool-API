using FluentValidation;
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
    public class UserValidator: ModelValidatorBase<UserModel>
    {
        public UserValidator(IUserManager manager, ValidationMode mode) :
            base(manager, mode)
        {
            RuleFor(user => user.Username).EmailAddress()
                .When(x => HasFlag(ValidationMode.DataRelevance))
                .WithMessage("{PropertyName} must be an email");
        }
    }
}
