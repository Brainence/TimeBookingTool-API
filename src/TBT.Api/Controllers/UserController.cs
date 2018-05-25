using System.Threading.Tasks;
using System.Web.Http;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.Filters.ControllersFilters;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TBT.Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : CrudApiController<UserModel>
    {
        public UserController(IManagerStore managerStore)
            : base(managerStore, managerStore.UserManager)
        { }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        [UserControllerValidationFilter]
        public async Task<UserModel> GetByEmail([Validator(ValidationMode.DataRelevance)]string email)
        {
            return ManagerStore.UserManager.GetByEmail(email);
        }

        [HttpGet]
        [Route("ValidatePassword/{id:int:min(1)}/{password}")]
        [UserControllerValidationFilter]
        public async Task<bool> IsPasswordValid([Validator(ValidationMode.Exist)]int id, string password)
        {
            return await ManagerStore.UserManager.IsPasswordValid(id, password);
        }

        [HttpGet]
        [Route("GetByCompany/{companyId:int:min(1)}")]
        [UserControllerValidationFilter]
        public async Task<List<UserModel>> GetByCompanyId([Validator(ValidationMode.Exist)]int companyId)
        {
            return await ManagerStore.UserManager.GetByCompanyIdAsync(companyId);
        }

        [HttpGet]
        [Route("ChangePassword/{id:int:min(1)}/{oldPassword}/{newPassword}")]
        [UserControllerValidationFilter]
        public async Task ChangePassword([Validator(ValidationMode.Exist)]int id, string oldPassword, string newPassword)
        {
            await ManagerStore.UserManager.ChangePassword(id, oldPassword, newPassword);
        }


    }
}
