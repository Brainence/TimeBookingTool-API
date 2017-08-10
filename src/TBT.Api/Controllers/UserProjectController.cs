using System.Web.Http;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;

namespace TBT.Api.Controllers
{
    [RoutePrefix("api/UserProject")]
    public class UserProjectController : BaseApiController
    {
        public UserProjectController(IManagerStore managerStore)
            : base(managerStore)
        { }

        [HttpPost]
        [Route("Add/{userId:int:min(1)}/{projectId:int:min(1)}")]
        public async void AddProject(int userId, int projectId)
        {
            await ManagerStore.UserProjectManager.AddProject(userId, projectId);
        }

        [HttpDelete]
        [Route("Remove/{userId:int:min(1)}/{projectId:int:min(1)}")]
        public async void RemoveProject(int userId, int projectId)
        {
            await ManagerStore.UserProjectManager.RemoveProject(userId, projectId);
        }
    }
}