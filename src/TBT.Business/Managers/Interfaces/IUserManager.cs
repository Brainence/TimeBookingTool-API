using System.Collections.Generic;
using System.Threading.Tasks;
using TBT.Business.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface IUserManager : ICrudManager<UserModel>
    {
        UserModel GetByEmail(string email);
        UserModel GetUserProject(string email);
        Task<bool> IsPasswordValid(int userId, string password);
        Task ChangePassword(int userId, string oldPassword, string newPassword);
        Task<List<UserModel>> GetByCompanyIdAsync(int companyId);
        Task<bool> SendEmail(EmailData data);
    }
}
