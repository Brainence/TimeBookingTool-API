using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IActivityRepository : IRepository, IRepository<Activity>
    {
        Task<Activity> GetByNameAsync(string name, int projectId);
        Task<List<Activity>> GetByCompanyIdAsync(int companyId);
        Task<List<Activity>> GetByProjectIdAsync(int projectId);

    }
}
