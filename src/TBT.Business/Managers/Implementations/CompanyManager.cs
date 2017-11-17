using NLog;
using System;
using System.Reflection;
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
    public class CompanyManager: CrudManager<Company, CompanyModel>, ICompanyManager
    {
        #region Constructors

        public CompanyManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, unitOfWork.Customers, objectMapper, configurationProvider, logger)
        {
        }

        #endregion

        #region Interface members



        #endregion
    }
}
