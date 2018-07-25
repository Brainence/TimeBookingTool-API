using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IUserRepository : IRepository, IRepository<User>
    {
        User GetByEmail(string email);
        User GetUserProject(string email);
        Task<bool> IsPasswordValid(int userId, string password);
        Task ChangePassword(int userId, string oldPassword, string newPassword);
        Task<IQueryable<User>> GetByCompanyId(int companyId);
    }
}
