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

        public async Task AddProjectAsync(int userId, int projectId)
        {
            await UnitOfWork.UserProjects.AddUserProjectAsync(userId, projectId);
            UnitOfWork.SaveChanges();
        }

        public async Task RemoveProjectAsync(int userId, int projectId)
        {
            await UnitOfWork.UserProjects.RemoveUserProjectAsync(userId, projectId);
            UnitOfWork.SaveChanges();
        }
    }

}
