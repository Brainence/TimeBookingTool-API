using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface ICompanyRepository: IRepository, IRepository<Company>
    {
        Task<Company> GetByName(string name);
    }
}
