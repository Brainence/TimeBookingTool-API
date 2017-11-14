﻿using System.Threading.Tasks;
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
        public UserModel GetByEmail([Validator(ValidationMode.DataRelevance)]string email)
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

        [UserControllerValidationFilter]
        [HttpGet]
        [Route("ChangePassword/{id:int:min(1)}/{oldPassword}/{newPassword}")]
        public async Task ChangePassword([Validator(ValidationMode.Exist)]int id, string oldPassword, string newPassword)
        {
            await ManagerStore.UserManager.ChangePassword(id, oldPassword, newPassword);
        }
    }
}
