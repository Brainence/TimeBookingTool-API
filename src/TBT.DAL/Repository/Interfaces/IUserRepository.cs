using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IUserRepository : IRepository, IRepository<User>
    {
        User GetByEmail(string email);
        Task<IQueryable<User>> GetByProjectAsync(int projectId);
        Task<bool> IsPasswordValid(int userId, string password);
        Task ChangePassword(int userId, string oldPassword, string newPassword);
    }
}
