using NLog;
using System;
using System.Reflection;
using System.Threading.Tasks;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.Components.Interfaces.Logger;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Managers.Implementations
{
    public class UserProjectManager : BaseManager, IUserProjectManager
    {
        public UserProjectManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, objectMapper, configurationProvider, logger)
        { }

        public async Task AddProject(int userId, int projectId)
        {
            try
            {
                await UnitOfWork.UserProjects.AddUserProject(userId, projectId);
                base.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameters: userId={userId}; projectId={projectId}");
            }
        }

        public async Task RemoveProject(int userId, int projectId)
        {
            try
            {
                await UnitOfWork.UserProjects.RemoveUserProject(userId, projectId);
                base.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                var x = MethodBase.GetCurrentMethod();
                Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameters: userId={userId}; projectId={projectId}");
            }
        }
    }

}
