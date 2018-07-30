using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IProjectRepository : IRepository, IRepository<Project>
    {
        Task<Project> GetByNameAsync(string name);
        Task<List<Project>> GetByCompanyIdAsync(int companyId);
    }
}
