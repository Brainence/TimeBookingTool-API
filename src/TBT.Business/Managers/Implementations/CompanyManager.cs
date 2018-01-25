using System.Threading.Tasks;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.Components.Interfaces.Logger;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace TBT.Business.Managers.Implementations
{
    public class CompanyManager: CrudManager<Company, CompanyModel>, ICompanyManager
    {
        #region Constructors

        public CompanyManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, unitOfWork.Companies, objectMapper, configurationProvider, logger)
        {
        }

        #endregion

        #region Interface members

        public async Task<CompanyModel> GetByName(string name)
        {
            return ObjectMapper.Map<Company, CompanyModel>(
                     await UnitOfWork.Companies.GetByName(name));
        }

        public async Task<int> RegisterCompany(UserModel superAdmin)
        {
            if(superAdmin?.Company == null) { return 0; }
            var entity = ObjectMapper.Map<UserModel, User>(superAdmin);
            entity.Password = new PasswordHasher().HashPassword(entity.Password);

            await Repository.AddAsync(ObjectMapper.Map<CompanyModel, Company>(superAdmin.Company));

            await UnitOfWork.Users.AddAsync(entity);

            await UnitOfWork.SaveChangesAsync();

            return entity.Id;
        }

        #endregion
    }
}
