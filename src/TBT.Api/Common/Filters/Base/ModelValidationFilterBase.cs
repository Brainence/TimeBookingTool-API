using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TBT.WebApi.Exceptions;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Api.Common.FluentValidation.Store.Interfaces;
using FluentValidation;
using TBT.Business.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace TBT.Api.Common.Filters.Base
{
    [Flags]
    public enum ValidationMode
    {
        None = 0,
        DataRelevance = 1,
        Exist = 2,
        Delete = 4,
        Update = 8,
        Add = 16
    }

    public class ModelValidationFilterBase: ActionFilterAttribute
    {
        protected IValidatorStore _validatorStore;

        public async override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (_validatorStore == null) { _validatorStore = ServiceLocator.Current.Get<IValidatorStore>(); }
            var type = default(Type);
            var model = default(object);
            foreach (var parameter in actionContext.ActionDescriptor.GetParameters().Where(x => x.GetCustomAttributes<object>().OfType<Validator>().Any()))
            {
                if (parameter.ParameterName == "id")
                {
                    type = ((ReflectedHttpActionDescriptor)actionContext.ActionDescriptor).MethodInfo.DeclaringType.GetGenericArguments()[0];
                    model = ServiceLocator.Current.Get<IModel>();
                    ((IModel)model).Id = (int)actionContext.ActionArguments[parameter.ParameterName];
                }
                if (parameter.ParameterType.IsSubclassOf(typeof(IModel)))
                {
                    type = actionContext.ActionArguments[parameter.ParameterName].GetType();
                    model = actionContext.ActionArguments[parameter.ParameterName];
                }
                if (type == default(Type)) { continue; }
                var attribute = parameter.GetCustomAttributes<object>().OfType<Validator>().FirstOrDefault();
                var validator = _validatorStore.GetValidator(attribute.Mode, type);
                var result = await validator.ValidateAsync(model);
                if (!result.IsValid)
                {
                    throw new ApiValidationException(string.Join(Environment.NewLine, result.Errors));
                }
            }
        }
    }
}