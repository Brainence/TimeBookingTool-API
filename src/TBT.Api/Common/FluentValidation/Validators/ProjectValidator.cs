using FluentValidation;
using TBT.Business.Models.BusinessModels;
using TBT.Api.Common.Filters.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Api.Common.FluentValidation.Base;
using TBT.Business.Infrastructure.CastleWindsor;

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class ProjectValidator: ModelValidatorBase<ProjectModel>
    {
        public ProjectValidator(IProjectManager manager, ValidationMode mode) :
            base(manager, mode)
        {
            RuleFor(project => project.Name).NotEmpty()
                .When(x => HasFlag(ValidationMode.Add | ValidationMode.Update | ValidationMode.DataRelevance))
                .WithMessage("{PropertyName} can't be null or empty.");
            RuleFor(project => project.IsActive).Equal(true)
                .When(x => HasFlag(ValidationMode.Add))
                .WithMessage("{PropertyName} can't be {PropertyValue}");
            RuleFor(project => project.Customer).NotNull()
                .MustAsync(async (x, token) => await ExistsAsync(x.Id, ServiceLocator.Current.Get<ICustomerManager>()))
                .When(x => HasFlag(ValidationMode.Add | ValidationMode.Update))
                .WithMessage("{PropertyName} can't be null or doesn't exist.");
            RuleFor(project => project)
                .MustAsync(async (x, token) =>
                {
                    var tempProject = await manager .GetByName(x.Name);
                    return tempProject == null || x.Id == tempProject.Id;
                })
                .When(x => HasFlag(ValidationMode.Add | ValidationMode.Update))
                .WithMessage("Project with this name already exists.");
        }
    }
}