using System.Collections.Generic;
using System.Threading.Tasks;
using TBT.Business.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface IActivityManager : ICrudManager<ActivityModel>
    {
        Task<ActivityModel> GetByName(string name, int projectId);
        Task<List<ActivityModel>> GetByCompanyIdAsync(int companyId);
        Task<List<ActivityModel>> GetByProjectIdAsync(int companyId);
    }
}
