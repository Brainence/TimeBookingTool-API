using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Interfaces
{
    public interface ICustomerRepository : IRepository, IRepository<Customer>
    {
        Task<Customer> GetByNameAsync(string name);
    }
}
