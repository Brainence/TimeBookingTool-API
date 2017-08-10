using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface IAccountManager 
    {
        Account GetByEmail(string email);
    }
}
