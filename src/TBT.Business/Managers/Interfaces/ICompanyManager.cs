using System.Threading.Tasks;
using TBT.Business.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface ICompanyManager: ICrudManager<CompanyModel>
    {
        Task<CompanyModel> GetByName(string name);

        Task<int> RegisterCompany(UserModel superAdmin);
    }
}
