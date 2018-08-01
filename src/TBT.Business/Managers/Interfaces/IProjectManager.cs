using System.Collections.Generic;
using System.Threading.Tasks;
using TBT.Business.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface IProjectManager : ICrudManager<ProjectModel>
    {
        Task<ProjectModel> GetByNameAsync(string name);
        Task<List<ProjectModel>> GetByCompanyIdAsync(int companyId);
        Task<List<ProjectModel>> GetByCompanyIdWithActivityAsync(int companyId);
    }
}
