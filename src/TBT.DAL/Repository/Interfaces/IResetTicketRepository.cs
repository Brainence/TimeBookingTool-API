using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IResetTicketRepository : IRepository, IRepository<ResetTicket>
    {
        Task<ResetTicket> CreateResetTicketAsync(int userId);
        Task<bool> ChangePasswordAsync(int userId, string newPassword, string token);
    }
}
