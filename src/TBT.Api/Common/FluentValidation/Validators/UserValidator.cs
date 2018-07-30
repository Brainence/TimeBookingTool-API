using FluentValidation;
using TBT.Business.Models.BusinessModels;
using TBT.Api.Common.FluentValidation.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Api.Common.Filters.Base;
using System.Threading.Tasks;

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class UserValidator : ModelValidatorBase<UserModel>
    {
        public UserValidator(IUserManager manager, ValidationMode mode) :
            base(manager, mode)
        {
            RuleFor(user => user.Username).EmailAddress()
                .When(x => HasFlag(ValidationMode.DataRelevance | ValidationMode.Add | ValidationMode.Update))
                .WithMessage("{PropertyName} must be an email");
            RuleFor(user => user)
                .MustAsync(async  (x, token) =>
                {
                    var tempUser = await manager.GetByEmail(x.Username);
                    return tempUser == null || x.Id == tempUser.Id;
                })
                .When(x => HasFlag(ValidationMode.Add | ValidationMode.Update))
                .WithMessage("User with this login already exists");

            RuleFor(user => user.MonthlySalary).
                MustAsync((x, token) => Task.FromResult(!x.HasValue || x >= 0)).
                When(x => HasFlag(ValidationMode.Update | ValidationMode.Add))
                .WithMessage("Salary must be above 0");
        }
    }
}
