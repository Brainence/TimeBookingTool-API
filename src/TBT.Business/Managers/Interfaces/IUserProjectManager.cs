using System.Threading.Tasks;
using TBT.Business.Interfaces;

namespace TBT.Business.Managers.Interfaces
{
    public interface IUserProjectManager
    {
        Task AddProject(int userId, int projectId);
        Task RemoveProject(int userId, int projectId);
    }
}
