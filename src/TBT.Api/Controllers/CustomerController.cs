using System.Threading.Tasks;
using System.Web.Http;
using TBT.Api.Controllers.Base;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;

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
    }
}
