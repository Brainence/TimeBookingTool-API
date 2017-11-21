using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.Filters.ControllersFilters;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.WebApi.Common.Filters;

namespace TBT.Api.Controllers
{
    [CommonActionFilter]
    [RoutePrefix("api/CompanyRegistration")]
    public class CompanyRegistrationController : BaseApiController
    {
        public CompanyRegistrationController(IManagerStore managerStore)
            : base(managerStore)
        { }

        [HttpPost]
        [Route("")]
        [CompanyRegistrationValidationFilter]
        [AllowAnonymous]
        public virtual async Task<HttpResponseMessage> RegisterCompany([Validator(ValidationMode.Add)]UserModel newUser)
        {
            newUser.IsAdmin = true;
            return Request.CreateResponse(HttpStatusCode.OK, await ManagerStore.CompanyManager.RegisterCompany(newUser));
        }
    }
}
