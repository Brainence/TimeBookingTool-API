using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IProjectRepository : IRepository, IRepository<Project>
    {
        Task<IQueryable<Project>> GetByUserAsync(int userId);
        Task<IQueryable<Project>> GetByCustomerAsync(int customerId);
        Task<IQueryable<Project>> GetByActivityAsync(int activityId);
        Task<Project> GetByName(string name);
    }
}
