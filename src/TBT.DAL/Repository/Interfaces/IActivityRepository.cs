using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IActivityRepository : IRepository, IRepository<Activity>
    {
        Task<Activity> GetByName(string name, int projectId);
        Task<IQueryable<Activity>> GetByCompanyIdAsync(int companyId);
        Task<IQueryable<Activity>> GetByProjectIdAsync(int projectId);

    }
}
