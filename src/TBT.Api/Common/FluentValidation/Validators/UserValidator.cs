using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBT.Business.Models.BusinessModels;
using TBT.Api.Common.FluentValidation.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Api.Common.Filters.Base;
using TBT.Business.Infrastructure.CastleWindsor;
using System.Threading.Tasks;

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class UserValidator: ModelValidatorBase<UserModel>
    {
        public UserValidator(IUserManager manager, ValidationMode mode) :
            base(manager, mode)
        {
            RuleFor(user => user.Username).EmailAddress()
                .When(x => HasFlag(ValidationMode.DataRelevance | ValidationMode.Add | ValidationMode.Update))
                .WithMessage("{PropertyName} must be an email");
            RuleFor(user => user.Username)
                .MustAsync((x, token) => Task.FromResult(((IUserManager)_manager).GetByEmail(x) == null))
                .When(x => HasFlag(ValidationMode.Add))
                .WithMessage("User with {PropertyValue} login already exists.");
        }
    }
}
