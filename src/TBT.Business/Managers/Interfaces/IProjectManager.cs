using System.Collections.Generic;
using System.Threading.Tasks;
using TBT.Business.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface IProjectManager : ICrudManager<ProjectModel>
    {
        Task<List<ProjectModel>> GetByUserAsync(int userId);
        Task<List<ProjectModel>> GetByCustomerAsync(int customerId);
        Task<List<ProjectModel>> GetByActivityAsync(int activityId);
        Task<ProjectModel> GetByName(string name);
    }
}
