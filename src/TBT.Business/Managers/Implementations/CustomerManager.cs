using System.Collections.Generic;
using System.Threading.Tasks;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.Logger;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;


namespace TBT.Business.Managers.Implementations
{
    public class CustomerManager : CrudManager<Customer, CustomerModel>, ICustomerManager
    {
        private readonly IManagerStore _store;
        #region Constructors

        public CustomerManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger, IManagerStore store)
            : base(unitOfWork, unitOfWork.Customers, objectMapper, configurationProvider, logger)
        {
            _store = store;
        }

        #endregion

        #region Interface Members

        public override async Task UpdateAsync(CustomerModel model)
        {
            if (!model.IsActive)
            {
                foreach (var project in model.Projects)
                {
                    project.IsActive = false;
                    project.Customer = model;
                    await _store.ProjectManager.UpdateAsync(project);
                }
            }
            await base.UpdateAsync(model);
        }

        public async Task<CustomerModel> GetByNameAsync(string name)
        {
            return ObjectMapper.Map<Customer, CustomerModel>(
                 await UnitOfWork.Customers.GetByNameAsync(name));
        }

        public async Task<List<CustomerModel>> GetByCompanyIdAsync(int companyId)
        {
            return ObjectMapper.Map<List<Customer>, List<CustomerModel>>(await UnitOfWork.Customers.GetByCompanyIdAsync(companyId));
        }

        public async Task<List<CustomerModel>> GetByCompanyIdWithActivitiesAsync(int companyId)
        {
            return ObjectMapper.Map<List<Customer>, List<CustomerModel>>(await UnitOfWork.Customers.GetByCompanyIdWithActivitiesAsync(companyId));
        }

        #endregion


    }
}
