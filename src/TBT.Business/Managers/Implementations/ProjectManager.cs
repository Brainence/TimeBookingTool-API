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
        public override void Dispose()
        {
            base.Dispose();
        }


        public async Task<List<ProjectModel>> GetByActivityAsync(int activityId)
        {
            //try
            //{
                return ObjectMapper.Map<IQueryable<Project>, List<ProjectModel>>(
                     await UnitOfWork.Projects.GetByActivityAsync(activityId));
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {activityId}");
            //    return null;
            //}
        }

        public async Task<List<ProjectModel>> GetByCustomerAsync(int customerId)
        {
            //try
            //{
                return ObjectMapper.Map<IQueryable<Project>, List<ProjectModel>>(
                     await UnitOfWork.Projects.GetByCustomerAsync(customerId));
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {customerId}");
            //    return null;
            //}
        }

        public async Task<ProjectModel> GetByName(string name)
        {
            //try
            //{
                return ObjectMapper.Map<Project, ProjectModel>(
                    await UnitOfWork.Projects.GetByName(name));
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {name}");
            //    return null;
            //}
        }

        public async Task<List<ProjectModel>> GetByUserAsync(int userId)
        {
            //try
            //{
                return ObjectMapper.Map<IQueryable<Project>, List<ProjectModel>>(
                     await UnitOfWork.Projects.GetByUserAsync(userId));
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {userId}");
            //    return null;
            //}
        }

        public async override Task UpdateAsync(ProjectModel model)
        {
            //try
            //{
                if (model.Customer == null)
                {
                    var project = await UnitOfWork.Projects.GetAsync(model.Id);
                    if (project == null || !project.CustomerId.HasValue) return;

                    var customer = await UnitOfWork.Customers.GetAsync(project.CustomerId.Value);
                    if (customer == null) return;

                    model.Customer = ObjectMapper.Map<Customer, CustomerModel>(customer);
                }

                await base.UpdateAsync(model);
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {model.ToString()}");
            //}
        }
        #endregion
    }
}
