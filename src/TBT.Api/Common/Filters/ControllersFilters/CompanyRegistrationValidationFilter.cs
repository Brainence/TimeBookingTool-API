using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Api.Common.FluentValidation.Store.Interfaces;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Business.Models.BusinessModels;
using TBT.WebApi.Exceptions;

namespace TBT.Api.Common.Filters.ControllersFilters
{
    public class CompanyRegistrationValidationFilter: ActionFilterAttribute
    {

        public async override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var model = default(UserModel);
            var _validatorStore = ServiceLocator.Current.Get<IValidatorStore>();
            foreach (var parameter in actionContext.ActionDescriptor.GetParameters().Where(x => x.GetCustomAttributes<object>().OfType<Validator>().Any()))
            {
                if (parameter.ParameterType == typeof(UserModel))
                {
                    model = actionContext.ActionArguments[parameter.ParameterName] as UserModel;
                    var attribute = parameter.GetCustomAttributes<object>().OfType<Validator>().FirstOrDefault();
                    var validator = _validatorStore.GetValidator(attribute.Mode, typeof(UserModel));
                    var result = await validator.ValidateAsync(model);
                    if (!result.IsValid)
                    {
                        throw new ApiValidationException(string.Join(Environment.NewLine, result.Errors));
                    }
                    validator = _validatorStore.GetValidator(attribute.Mode, typeof(CompanyModel));
                    result = await validator.ValidateAsync(model.Company);
                    if (!result.IsValid)
                    {
                        throw new ApiValidationException(string.Join(Environment.NewLine, result.Errors));
                    }
                }
            }
        }
    }
}