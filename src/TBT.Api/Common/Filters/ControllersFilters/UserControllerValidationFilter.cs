using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Business.Models.BusinessModels;
using TBT.WebApi.Exceptions;

namespace TBT.Api.Common.Filters.ControllersFilters
{
    public class UserControllerValidationFilter: ModelBaseValidationFilter
    {


        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            await base.OnActionExecutingAsync(actionContext, cancellationToken);
            var model = default(UserModel);
            foreach (var parameter in actionContext.ActionDescriptor.GetParameters().Where(x => x.GetCustomAttributes<object>().OfType<Validator>().Any()))
            {
                if (parameter.ParameterName == "email")
                {
                    model = new UserModel() { Username = actionContext.ActionArguments[parameter.ParameterName].ToString() };
                }
                var attribute = parameter.GetCustomAttributes<object>().OfType<Validator>().FirstOrDefault();
                var validator = _validatorStore.GetValidator(attribute.Mode, typeof(UserModel));
                var result = await validator.ValidateAsync(model);
                if (!result.IsValid)
                {
                    throw new ApiValidationException(string.Join("\r\n", result.Errors));
                }
            }
        }
    }
}