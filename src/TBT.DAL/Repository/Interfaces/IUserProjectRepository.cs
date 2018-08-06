using System.Threading.Tasks;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IUserProjectRepository : IRepository
    {
        Task AddUserProjectAsync(int userId, int projectId);
        Task RemoveUserProjectAsync(int userId, int projectId);
    }
}
