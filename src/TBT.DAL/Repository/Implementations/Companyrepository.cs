using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context)
            : base(context)
        {
        }

        #region Interface members

        public Task<Company> GetByName(string name)
        {
            return Task.FromResult(DbSet
                .Where(x => x.IsActive)
                .Include(x => x.Users)
                .Include(x => x.Customers)
                .FirstOrDefault(x => x.CompanyName == name));
        }

        #endregion
    }
}