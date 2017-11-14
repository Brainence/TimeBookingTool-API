using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Api.Common.FluentValidation.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Interfaces;
using TBT.WebApi.Exceptions;

namespace TBT.Api.Common.Filters.ControllersFilters
{
    public class TimeEntryControllerValidationFilter: ModelValidationFilterBase
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
                if(parameter.ParameterName == "date" && attribute.Mode.HasFlag(ValidationMode.DataRelevance))
                {
                    var temp = default(DateTime);
                    if(!DateTime.TryParse((string)actionContext.ActionArguments[parameter.ParameterName], out temp))
                    {
                        throw new ApiValidationException("Wrong format of dateString.");
                    }
                }
                if (!result.IsValid)
                {
                    throw new ApiValidationException(string.Join(Environment.NewLine, result.Errors));
                }
            }
        }
    }
}