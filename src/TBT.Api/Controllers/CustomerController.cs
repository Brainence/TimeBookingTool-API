using System.Threading.Tasks;
using System.Web.Http;
using TBT.Api.Common.Filters.Base;
using TBT.Api.Common.Filters.ControllersFilters;
using TBT.Api.Common.FluentValidation.Attributes;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using System.Collections.Generic;

namespace TBT.Api.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : CrudApiController<CustomerModel>
    {
        public CustomerController(IManagerStore managerStore)
            : base(managerStore, managerStore.CustomerManager)
        { }

        [HttpGet]
        [Route("GetByName/{name}")]
        public async Task<CustomerModel> GetByNameAsync(string name)
        {
            return await ManagerStore.CustomerManager.GetByNameAsync(name);
        }

        [HttpGet]
        [Route("GetByCompany/{companyId:int:min(1)}")]
        [CustomerControllerValidationFilter]
        public async Task<List<CustomerModel>> GetByCompanyAsync([Validator(ValidationMode.Exist)]int companyId)
        {
            return await ManagerStore.CustomerManager.GetByCompanyIdAsync(companyId);
        }
    }
}
