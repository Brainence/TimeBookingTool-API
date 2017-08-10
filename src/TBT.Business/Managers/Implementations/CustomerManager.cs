using System.Threading.Tasks;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Managers.Implementations
{
    public class CustomerManager : CrudManager<Customer, CustomerModel>, ICustomerManager
    {
        #region Constructors

        public CustomerManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider)
            : base(unitOfWork, unitOfWork.Customers, objectMapper, configurationProvider)
        {
        }

        #endregion

        #region Interface Members

        public async Task<CustomerModel> GetByNameAsync(string name)
        {
            return ObjectMapper.Map<Customer, CustomerModel>(
                 await UnitOfWork.Customers.GetByNameAsync(name));
        }

        #endregion


    }
}
