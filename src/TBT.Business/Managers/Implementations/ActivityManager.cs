using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public class ActivityManager : CrudManager<Activity, ActivityModel>, IActivityManager
    {
        #region Constructors

        public ActivityManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, unitOfWork.Activities, objectMapper, configurationProvider, logger)
        { }

        #endregion

        #region Interface Members
        public override void Dispose()
        {
            base.Dispose();
        }

        public async Task<ActivityModel> GetByName(string name, int projectId)
        {
            try
            {
                return ObjectMapper.Map<Activity, ActivityModel>(
                     await UnitOfWork.Activities.GetByName(name, projectId));
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameters: name={name}; projectId={projectId}");
                return null;
            }
        }

        public async Task<List<ActivityModel>> GetByProjectAsync(int id)
        {
            try
            {
                return ObjectMapper.Map<IQueryable<Activity>, List<ActivityModel>>(
                     await UnitOfWork.Activities.GetByProjectAsync(id));
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {id}");
                return null;
            }
        }

        public async override Task UpdateAsync(ActivityModel model)
        {
            try
            {
                if (model.Project == null)
                {
                    var activity = await UnitOfWork.Activities.GetAsync(model.Id);
                    if (activity == null || !activity.ProjectId.HasValue) return;

                    var project = await UnitOfWork.Projects.GetAsync(activity.ProjectId.Value);
                    if (project == null) return;

                    model.Project = ObjectMapper.Map<Project, ProjectModel>(project);
                }

                await base.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {model.ToString()}");
            }
        }
        #endregion
    }
}
