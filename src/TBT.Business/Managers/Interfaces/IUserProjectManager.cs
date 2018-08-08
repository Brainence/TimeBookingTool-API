using System.Threading.Tasks;

namespace TBT.Business.Managers.Interfaces
{
    public interface IUserProjectManager
    {
        Task AddProjectAsync(int userId, int projectId);
        Task RemoveProjectAsync(int userId, int projectId);
    }
}
