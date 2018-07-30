using System.Threading.Tasks;
using TBT.Business.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Business.Providers.Interfaces;
using TBT.Components.Interfaces.ObjectMapper;
using TBT.Components.Interfaces.Logger;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.Business.Managers.Implementations
{
    public class AccountManager : BaseManager, IAccountManager
    {
        #region Constructors

        public AccountManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, objectMapper, configurationProvider, logger)
        { }

        #endregion

        #region Interface Members

        public async Task<Account> GetByEmail(string email)
        {
            return ObjectMapper.Map<User, Account>(await UnitOfWork.Users.GetByEmailAsync(email));
        }

        #endregion
    }
}
