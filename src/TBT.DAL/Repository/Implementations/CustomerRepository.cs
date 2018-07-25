using System.Data.Entity;
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
            return Task.FromResult(
                DbSet.FirstOrDefault(c => c.Name == name && c.IsActive));
        }

        public Task<IQueryable<Customer>> GetByCompanyIdAsync(int companyId)
        {
            return Task.FromResult(DbSet
                .IncludeFilter(x => x.Projects.Where(y => y.IsActive))
                .IncludeFilter(x => x.Projects.Select(y => y.Activities.Where(z => z.IsActive)))             
                .Where(x => x.IsActive && x.CompanyId == companyId));
        }
    }
}
