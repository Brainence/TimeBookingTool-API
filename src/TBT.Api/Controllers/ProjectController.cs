using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.WebApi.Common.Filters;

namespace TBT.Api.Controllers
{    
    [CommonActionFilter]
    [RoutePrefix("api/Project")]
    public class ProjectController : CrudApiController<ProjectModel>
    {
        public ProjectController(IManagerStore managerStore) 
            : base(managerStore, managerStore.ProjectManager)
        { }

        [HttpGet]
        [Route("GetByUser/{userId:int:min(1)}")]
        public async Task<List<ProjectModel>> GetByUserAsync(int userId)
        {
            return await ManagerStore.ProjectManager.GetByUserAsync(userId);
        }


        [HttpGet]
        [Route("GetByCustomer/{customerId:int:min(1)}")]
        public async Task<List<ProjectModel>> GetByCustomerAsync(int customerId)
        {
            return await ManagerStore.ProjectManager.GetByCustomerAsync(customerId);
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public async Task<ProjectModel> GetByName(string name)
        {
            return await ManagerStore.ProjectManager.GetByName(name);
        }
    }
}
