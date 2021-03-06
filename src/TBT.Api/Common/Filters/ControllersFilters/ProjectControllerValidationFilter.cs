﻿using System;
using System.Linq;
using System.Web.Http.Controllers;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Api.Common.FluentValidation.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.WebApi.Exceptions;
using TBT.Business.Interfaces;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace TBT.Api.Common.Filters.ControllersFilters
{
    public class ProjectControllerValidationFilter: ModelValidationFilterBase
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
                else if (parameter.ParameterName == "customerId")
                {
                    model = new CustomerModel() { Id = (int)actionContext.ActionArguments[parameter.ParameterName] };
                    validator = _validatorStore.GetValidator(attribute.Mode, typeof(CustomerModel));
                    result = await validator.ValidateAsync((CustomerModel)model);
                }
                else if (parameter.ParameterName == "companyId")
                {
                    model = new CompanyModel() { Id = (int)actionContext.ActionArguments[parameter.ParameterName] };
                    validator = _validatorStore.GetValidator(attribute.Mode, typeof(CompanyModel));
                    result = await validator.ValidateAsync((CompanyModel)model);
                }
                else if (parameter.ParameterName == "name")
                {
                    model = new ProjectModel() { Name = actionContext.ActionArguments[parameter.ParameterName].ToString() };
                    validator = _validatorStore.GetValidator(attribute.Mode, typeof(ProjectModel));
                    result = await validator.ValidateAsync((ProjectModel)model);
                }
                if (!result.IsValid)
                {
                    throw new ApiValidationException(string.Join(Environment.NewLine, result.Errors));
                }
            }
        }
    }
}