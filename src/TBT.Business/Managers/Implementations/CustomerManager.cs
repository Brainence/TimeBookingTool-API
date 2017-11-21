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
using System.Collections.Generic;
using System.Linq;

namespace TBT.Business.Managers.Implementations
{
    public class CustomerManager : CrudManager<Customer, CustomerModel>, ICustomerManager
    {
        #region Constructors

        public CustomerManager(
            IApplicationUnitOfWork unitOfWork,
            IObjectMapper objectMapper,
            IConfigurationProvider configurationProvider, ILogManager logger)
            : base(unitOfWork, unitOfWork.Customers, objectMapper, configurationProvider, logger)
        {
        }

        #endregion

        #region Interface Members

        public async Task<CustomerModel> GetByNameAsync(string name)
        {
            //try
            //{
                return ObjectMapper.Map<Customer, CustomerModel>(
                     await UnitOfWork.Customers.GetByNameAsync(name));
            //}
            //catch (Exception ex)
            //{
            //    var x = MethodBase.GetCurrentMethod();
            //    Logger.Error(ex, $"{ex.Message} {ex.InnerException?.Message}\nObjectType: {this.GetType()}\nMethod: {x.Name}\nParameter: {name}");
            //    return null;
            //}
        }

        public async Task<List<CustomerModel>> GetByCompanyIdAsync(int companyId)
        {
            return ObjectMapper.Map<IQueryable<Customer>, List<CustomerModel>>(
                     await UnitOfWork.Customers.GetByCompanyIdAsync(companyId));
        }

        #endregion


    }
}
