using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProjectManager : CrudManager<Project, ProjectModel>, IProjectManager
    {
        #region Constructors

        public ProjectManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider)
            : base(unitOfWork, unitOfWork.Projects, objectMapper, configurationProvider)
        {
        }

        #endregion

        #region Interface Members
        public override void Dispose()
        {
            base.Dispose();
        }


        public async Task<List<ProjectModel>> GetByActivityAsync(int activityId)
        {
            return ObjectMapper.Map<IQueryable<Project>, List<ProjectModel>>(
                 await UnitOfWork.Projects.GetByActivityAsync(activityId));
        }

        public async Task<List<ProjectModel>> GetByCustomerAsync(int customerId)
        {
            return ObjectMapper.Map<IQueryable<Project>, List<ProjectModel>>(
                 await UnitOfWork.Projects.GetByCustomerAsync(customerId));
        }

        public async Task<ProjectModel> GetByName(string name)
        {
            return ObjectMapper.Map<Project, ProjectModel>(
                await UnitOfWork.Projects.GetByName(name));
        }

        public async Task<List<ProjectModel>> GetByUserAsync(int userId)
        {
            return ObjectMapper.Map<IQueryable<Project>, List<ProjectModel>>(
                 await UnitOfWork.Projects.GetByUserAsync(userId));
        }
        public async override Task UpdateAsync(ProjectModel model)
        {
            if (model.Customer == null)
            {
                var project = await UnitOfWork.Projects.GetAsync(model.Id);
                if (project == null || !project.CustomerId.HasValue) return;

                var customer = await UnitOfWork.Customers.GetAsync(project.CustomerId.Value);
                if (customer == null) return;

                model.Customer = ObjectMapper.Map<Customer, CustomerModel>(customer);
            }

            await base.UpdateAsync(model);
        }
        #endregion
    }
}
