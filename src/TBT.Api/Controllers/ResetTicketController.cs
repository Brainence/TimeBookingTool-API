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
    [AllowAnonymous]
    [RoutePrefix("api/resetticket")]
    public class ResetTicketController : CrudApiController<ResetTicketModel>
    {
        public ResetTicketController(IManagerStore managerStore)
            : base(managerStore, managerStore.ResetTicketManager)
        { }

        [HttpGet]
        [Route("CreateResetTicket/{userId:int:min(1)}")]
        [AllowAnonymous]
        [ResetTicketControllerValidationFilter]
        public async Task<bool> CreateResetTicket([Validator(ValidationMode.Exist)]int userId)
        {
            return await ManagerStore.ResetTicketManager.CreateResetTicket(userId);
        }

        [HttpGet]
        [Route("ChangePassword/{userId:int:min(1)}/{newPassword}/{token}")]
        [AllowAnonymous]
        [ResetTicketControllerValidationFilter]
        public async Task<bool> ChangePassword([Validator(ValidationMode.Exist)]int userId, string newPassword, string token)
        {
            return await ManagerStore.ResetTicketManager.ChangePassword(userId, newPassword, token);
        }
    }
}
