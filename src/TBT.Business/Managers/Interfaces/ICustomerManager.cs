﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TBT.Business.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Business.Managers.Interfaces
{
    public interface ICustomerManager : ICrudManager<CustomerModel>
    {
        Task<CustomerModel> GetByNameAsync(string name);
        Task<List<CustomerModel>> GetByCompanyIdAsync(int companyId);
        Task<List<CustomerModel>> GetByCompanyIdWithActivitiesAsync(int companyId);
    }
}
