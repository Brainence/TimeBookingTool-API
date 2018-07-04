using System.Threading.Tasks;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.Components.Interfaces.Logger;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace TBT.Business.Managers.Implementations
{
    public class CustomerManager : CrudManager<Customer, CustomerModel>, ICustomerManager
    {

        #region Constructors

        public CustomerManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, unitOfWork.Customers, objectMapper, configurationProvider, logger)
        {

        }

        #endregion

        #region Interface Members

        public override async Task UpdateAsync(CustomerModel model)
        {
            if (!model.IsActive)
            {
                var projects = ObjectMapper.Map<List<ProjectModel>, List<Project>>(model.Projects);
                foreach (var project in projects)
                {
                    var activities = await UnitOfWork.Activities.GetByProjectIdAsync(project.Id);
                    project.IsActive = false;
                    project.CustomerId = model.Id;
                    foreach (var activity in activities)
                    {
                        activity.IsActive = false;
                        activity.ProjectId = project.Id;
                        await UnitOfWork.Activities.UpdateAsync(activity);
                    }
                    await UnitOfWork.Projects.UpdateAsync(project);
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
            return ObjectMapper.Map<IQueryable<Customer>, List<CustomerModel>>(
                     await UnitOfWork.Customers.GetByCompanyIdAsync(companyId));
        }
        #endregion


    }
}
