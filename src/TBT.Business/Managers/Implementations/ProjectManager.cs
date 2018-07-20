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
            //TODO strange iqueryable  maping
            var temp = await UnitOfWork.Projects.GetByCompanyIdAsync(companyId);
            return ObjectMapper.Map<List<Project>, List<ProjectModel>>(temp.ToList());  
        }

        public async Task<ProjectModel> GetByName(string name)
        {
            return ObjectMapper.Map<Project, ProjectModel>(
                await UnitOfWork.Projects.GetByName(name));
        }

        public override async Task UpdateAsync(ProjectModel model)
        {
            if (!model.IsActive)
            {
                var activities = ObjectMapper.Map<List<ActivityModel>, List<Activity>>(model.Activities);
                foreach (var activity in activities)
                {
                    activity.IsActive = false;
                    activity.ProjectId = model.Id;
                    await UnitOfWork.Activities.UpdateAsync(activity);
                }
            }
            await base.UpdateAsync(model);
        }

        #endregion
    }
}
