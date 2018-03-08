using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IActivityRepository : IRepository, IRepository<Activity>
    {
        Task<IQueryable<Activity>> GetByProjectAsync(int id);
        Task<Activity> GetByName(string name, int projectId);
        Task<IQueryable<Activity>> GetByCompanyIdAsync(int companyId);
    }
}
