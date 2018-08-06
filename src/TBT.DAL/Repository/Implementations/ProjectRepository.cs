using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;
using Z.EntityFramework.Plus;

namespace TBT.DAL.Repository.Implementations
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context)
        { }

        public Task<List<Project>> GetByCompanyIdAsync(int companyId)
        {
            return
                DbSet
                .Include(x => x.Customer)
                .Where(p => p.Customer.CompanyId == companyId).ToListAsync();
        }

        public Task<List<Project>> GetByCompanyIdWhithActivityAsync(int companyId)
        {
            return
                DbSet
                    .Include(x => x.Customer)
                    .Include(x => x.Activities)
                    .Where(p => p.Customer.CompanyId == companyId).ToListAsync();
        }

        public Task<Project> GetByNameAsync(string name)
        {
            return DbSet.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
