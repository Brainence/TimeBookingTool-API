using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Api.Common.Filters.ControllersFilters;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.FluentValidation.Attributes;

namespace TBT.Api.Controllers
{    
    [RoutePrefix("api/Project")]
    public class ProjectController : CrudApiController<ProjectModel>
    {
        public ProjectController(IManagerStore managerStore) 
            : base(managerStore, managerStore.ProjectManager)
        { }

        [HttpGet]
        [Route("GetByCompany/{companyId:int:min(1)}")]
        [ProjectControllerValidationFilter]
        public async Task<List<ProjectModel>> GetByCompanyAsync([Validator(ValidationMode.Exist)]int companyId)
        {
            return await ManagerStore.ProjectManager.GetByCompanyIdAsync(companyId);
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        [ProjectControllerValidationFilter]
        public async Task<ProjectModel> GetByName([Validator(ValidationMode.DataRelevance)]string name)
        {
            return await ManagerStore.ProjectManager.GetByName(name);
        }
    }
}
