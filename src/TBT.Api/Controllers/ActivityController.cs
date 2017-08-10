using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Api.Controllers
{
    [RoutePrefix("api/Activity")]
    public class ActivityController : CrudApiController<ActivityModel>
    {
        public ActivityController(IManagerStore managerStore) 
            : base(managerStore, managerStore.ActivityManager)
        { }

        [HttpGet]
        [Route("GetByProject/{projectId:int:min(1)}")]
        public async Task<List<ActivityModel>> GetByProjectAsync(int projectId)
        {
            return await ManagerStore.ActivityManager.GetByProjectAsync(projectId);
        }

        [HttpGet]
        [Route("GetByName/{name}/{projectId}")]
        public async Task<ActivityModel> GetByName(string name, int projectId)
        {
            return await ManagerStore.ActivityManager.GetByName(name, projectId);
        }
    }
}
