using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IResetTicketRepository : IRepository, IRepository<ResetTicket>
    {
        Task<ResetTicket> CreateResetTicket(int userId);
        Task<bool> ChangePassword(int userId, string newPassword, string token);
    }
}
