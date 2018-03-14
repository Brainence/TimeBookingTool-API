using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

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
                .Include(x => x.Projects)
                .Where(x => x.IsActive && x.CompanyId == companyId));
        }
    }
}
