using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IUserRepository : IRepository, IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetUserWithProjectAsync(string email);
        Task<bool> IsPasswordValidAsync(int userId, string password);
        Task ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        Task<List<User>> GetByCompanyIdAsync(int companyId);
    }
}
