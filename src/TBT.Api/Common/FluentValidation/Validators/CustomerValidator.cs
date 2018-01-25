using FluentValidation;
using TBT.Business.Models.BusinessModels;
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
            RuleFor(customer => customer.Name).NotEmpty()
                .When(x => HasFlag(ValidationMode.Add | ValidationMode.Update))
                .WithMessage("{PropertyName} can't be null or empty.");
            RuleFor(customer => customer.IsActive).Equal(true)
                .When(x => HasFlag(ValidationMode.Add))
                .WithMessage("{PropertyName} can't be {PropertyValue}.");
        }
    }
}