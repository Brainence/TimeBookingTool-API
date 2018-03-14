using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.Components.Interfaces.Logger;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Managers.Implementations
{
    public class ProjectManager : CrudManager<Project, ProjectModel>, IProjectManager
    {
        #region Constructors

        public ProjectManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, unitOfWork.Projects, objectMapper, configurationProvider, logger)
        {
        }

        #endregion

        #region Interface Members

        public async Task<List<ProjectModel>> GetByCompanyIdAsync(int companyId)
        {
            return ObjectMapper.Map<IQueryable<Project>, List<ProjectModel>>(
                     await UnitOfWork.Projects.GetByCompanyIdAsync(companyId));
        }

        public async Task<ProjectModel> GetByName(string name)
        {
            return ObjectMapper.Map<Project, ProjectModel>(
                await UnitOfWork.Projects.GetByName(name));
        }

        public override async Task UpdateAsync(ProjectModel model)
        {
            if (model.Customer == null)
            {
                var project = await UnitOfWork.Projects.GetAsync(model.Id);
                if (project?.CustomerId == null) return;

                var customer = await UnitOfWork.Customers.GetAsync(project.CustomerId.Value);
                if (customer == null) return;

                model.Customer = ObjectMapper.Map<Customer, CustomerModel>(customer);
            }

            await base.UpdateAsync(model);
        }

        #endregion
    }
}
