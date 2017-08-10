using System.Collections.Generic;
using System.Threading.Tasks;
using TBT.Business.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface IActivityManager : ICrudManager<ActivityModel>
    {
        Task<List<ActivityModel>> GetByProjectAsync(int id);
        Task<ActivityModel> GetByName(string name, int projectId);
    }
}
