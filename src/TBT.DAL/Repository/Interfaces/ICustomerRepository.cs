using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface ICustomerRepository : IRepository, IRepository<Customer>
    {
        Task<Customer> GetByNameAsync(string name);
        Task<IQueryable<Customer>> GetByCompanyIdAsync(int companyId);
    }
}
