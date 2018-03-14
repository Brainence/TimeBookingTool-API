using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TBT.Api.Common.Filters.ControllersFilters;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Api.Common.Filters.Base;

namespace TBT.Api.Controllers
{
    [RoutePrefix("api/Activity")]
    public class ActivityController : CrudApiController<ActivityModel>
    {
        public ActivityController(IManagerStore managerStore) 
            : base(managerStore, managerStore.ActivityManager)
        { }

        [HttpGet]
        [Route("GetByCompany/{companyId:int:min(1)}")]
        [ActivityControllerValidationFilter]
        public async Task<List<ActivityModel>> GetByCompanyAsync([Validator(ValidationMode.Exist)]int companyId)
        {
            return await ManagerStore.ActivityManager.GetByCompanyIdAsync(companyId);
        }

        [HttpGet]
        [Route("GetByName/{name}/{projectId:int:min(1)}")]
        [ActivityControllerValidationFilter]
        public async Task<ActivityModel> GetByName([Validator(ValidationMode.DataRelevance)]string name, [Validator(ValidationMode.Exist)]int projectId)
        {
            return await ManagerStore.ActivityManager.GetByName(name, projectId);
        }
    }
}
