using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Components.Interfaces.Logger;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;
using IConfigurationProvider = TBT.Business.Providers.Interfaces.IConfigurationProvider;
using IObjectMapper = TBT.Components.Interfaces.ObjectMapper.IObjectMapper;

namespace TBT.Business.Managers.Implementations
{
    public class ProjectManager : CrudManager<Project, ProjectModel>, IProjectManager
    {
        private readonly IManagerStore _store;
        #region Constructors

        public ProjectManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger,IManagerStore store)
            : base(unitOfWork, unitOfWork.Projects, objectMapper, configurationProvider, logger)
        {
            _store = store;
        }

        #endregion

        #region Interface Members

        public async Task<List<ProjectModel>> GetByCompanyIdAsync(int companyId)
        {
            return ObjectMapper.Map<List<Project>, List<ProjectModel>>(await UnitOfWork.Projects.GetByCompanyIdAsync(companyId));  
        }
        public async Task<List<ProjectModel>> GetByCompanyIdWithActivityAsync(int companyId)
        {
            return ObjectMapper.Map<List<Project>, List<ProjectModel>>(await UnitOfWork.Projects.GetByCompanyIdWhithActivityAsync(companyId));
        }

        public async Task<ProjectModel> GetByNameAsync(string name)
        {
            return ObjectMapper.Map<Project, ProjectModel>(await UnitOfWork.Projects.GetByNameAsync(name));
        }
        public override async Task UpdateAsync(ProjectModel model)
        {
            if (!model.IsActive)
            {
                foreach (var activity in await _store.ActivityManager.GetByProjectIdAsync(model.Id))
                {
                    activity.IsActive = false;
                    activity.Project = model;
                    await _store.ActivityManager.UpdateAsync(activity);
                }
            }
            await base.UpdateAsync(model);
        }

        #endregion
    }
}
