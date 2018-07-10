using FluentValidation;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.FluentValidation.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class CompanyValidator : ModelValidatorBase<CompanyModel>
    {
        public CompanyValidator(ICompanyManager manager, ValidationMode mode) : base(manager, mode)
        {
            RuleFor(company => company.CompanyName)
                .MustAsync(async (x, token) => await manager.GetByName(x) == null)
                .When(x => HasFlag(ValidationMode.Add | ValidationMode.Update | ValidationMode.DataRelevance))
                .WithMessage("Company with name {PropertyValue} is already exists.");
        }
    }
}