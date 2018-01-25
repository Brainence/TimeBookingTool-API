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
    public class ResetTicketControllerValidationFilter : ModelValidationFilterBase
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
                if (parameter.ParameterName == "userId")
                {
                    model = new UserModel() { Id = (int)actionContext.ActionArguments[parameter.ParameterName] };
                    validator = _validatorStore.GetValidator(attribute.Mode, typeof(UserModel));
                    result = await validator.ValidateAsync((UserModel)model);
                }
                if (!result.IsValid)
                {
                    throw new ApiValidationException(string.Join(Environment.NewLine, result.Errors));
                }
            }
        }
    }
}