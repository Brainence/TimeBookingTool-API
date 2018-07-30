using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;
using Z.EntityFramework.Plus;

namespace TBT.DAL.Repository.Implementations
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context)
            : base(context)
        { }

        public Task<Customer> GetByNameAsync(string name)
        {
            return DbSet.FirstOrDefaultAsync(c => c.Name == name);
        }


        public Task<List<Customer>> GetByCompanyIdAsync(int companyId)
        {
            return 
                DbSet
                .Include(x => x.Projects.Select(y => y.Activities))
                .Where(x => x.CompanyId == companyId).ToListAsync();
        }
    }
}
