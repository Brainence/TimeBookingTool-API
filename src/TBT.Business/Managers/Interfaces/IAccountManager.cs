using System.Threading.Tasks;
using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface IAccountManager 
    {
        Task<Account> GetByEmail(string email);
    }
}
