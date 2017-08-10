using System.Threading.Tasks;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IUserProjectRepository : IRepository
    {
        Task AddUserProject(int userId, int projectId);
        Task RemoveUserProject(int userId, int projectId);
    }
}
