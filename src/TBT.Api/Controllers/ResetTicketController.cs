using System.Threading.Tasks;
using System.Web.Http;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Api.Controllers
{
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
        public async Task<bool> CreateResetTicket(int userId)
        {
            return await ManagerStore.ResetTicketManager.CreateResetTicket(userId);
        }

        [HttpGet]
        [Route("ChangePassword/{userId:int:min(1)}/{newPassword}/{token}")]
        [AllowAnonymous]
        public async Task<bool> ChangePassword(int userId, string newPassword, string token)
        {
            return await ManagerStore.ResetTicketManager.ChangePassword(userId, newPassword, token);
        }
    }
}
