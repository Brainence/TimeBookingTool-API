using FluentValidation.Results;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Api.Common.FluentValidation.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Interfaces;
using TBT.WebApi.Exceptions;

namespace TBT.Api.Common.Filters.ControllersFilters
{
    public class ActivityControllerValidationFilter: ModelValidationFilterBase
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            await base.OnActionExecutingAsync(actionContext, cancellationToken);
            var model = default(IModel);
            var attribute = default(Validator);
            var validator = default(IModelValidatorBase);
            var result = new ValidationResult();
            foreach (var parameter in actionContext.ActionDescriptor.GetParameters().Where(x => x.GetCustomAttributes<object>().OfType<Validator>().Any()))
            {
                attribute = parameter.GetCustomAttributes<object>().OfType<Validator>().FirstOrDefault();
                if (parameter.ParameterName == "projectId")
                {
                    model = new ProjectModel() { Id = (int)actionContext.ActionArguments[parameter.ParameterName] };
                    validator = _validatorStore.GetValidator(attribute.Mode, typeof(ProjectModel));
                    result = await validator.ValidateAsync((ProjectModel)model);
                }
                else if (parameter.ParameterName == "companyId")
                {
                    model = new CompanyModel() { Id = (int)actionContext.ActionArguments[parameter.ParameterName] };
                    validator = _validatorStore.GetValidator(attribute.Mode, typeof(CompanyModel));
                    result = await validator.ValidateAsync((CompanyModel)model);
                }
                else if (parameter.ParameterName == "name")
                {
                    model = new ActivityModel() { Name = actionContext.ActionArguments[parameter.ParameterName].ToString() };
                    validator = _validatorStore.GetValidator(attribute.Mode, typeof(ActivityModel));
                    result = await validator.ValidateAsync((ActivityModel)model);
                }
                if (!result.IsValid)
                {
                    throw new ApiValidationException(string.Join(Environment.NewLine, result.Errors));
                }
            }
        }
    }
}