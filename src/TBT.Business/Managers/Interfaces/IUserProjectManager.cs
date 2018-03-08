using System.Threading.Tasks;

namespace TBT.Business.Managers.Interfaces
{
    public interface IUserProjectManager
    {
        Task AddProject(int userId, int projectId);
        Task RemoveProject(int userId, int projectId);
    }
}
