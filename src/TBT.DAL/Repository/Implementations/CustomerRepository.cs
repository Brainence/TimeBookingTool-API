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
                DbSet.Include(x => x.Projects).Include(u => u.Company).FirstOrDefault(c => c.Name == name && c.IsActive));
        }

        public override Task<IQueryable<Customer>> GetAsync()
        {
            return Task.FromResult(DbSet.Include(u => u.Company).Where(c => c.IsActive));
        }
        public override Task<Customer> GetAsync(int id)
        {
            return Task.FromResult(DbSet.Include(u => u.Company).Where(c => c.IsActive).FirstOrDefault(c => c.Id == id));
        }

        public Task<IQueryable<Customer>> GetByCompanyIdAsync(int companyId)
        {
            return Task.FromResult(DbSet
                .Include(x => x.Company)
                .Include(x => x.Projects)
                .Where(x => x.IsActive && x.CompanyId == companyId));
        }
    }
}
