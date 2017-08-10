using System.Threading.Tasks;
using TBT.Business.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface IResetTicketManager : ICrudManager<ResetTicketModel>
    {
        Task<bool> CreateResetTicket(int userId);
        Task<bool> ChangePassword(int userId, string newPassword, string token);
    }
}
