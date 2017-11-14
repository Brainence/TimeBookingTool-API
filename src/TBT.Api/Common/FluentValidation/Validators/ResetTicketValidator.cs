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
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Business.Managers.Interfaces;
using TBT.Api.Common.FluentValidation.Base;
using FluentValidation;

namespace TBT.Api.Common.FluentValidation.Validators
{
    public class ResetTicketValidator: ModelValidatorBase<ResetTicketModel>
    {
        public ResetTicketValidator(IResetTicketManager manager, ValidationMode mode) :
            base(manager, mode)
        {
            RuleFor(x => x.Username)
                .MustAsync(async (x, token) => await Task.FromResult(ServiceLocator.Current.Get<IUserManager>().GetByEmail(x)) != null);
        }
    }
}