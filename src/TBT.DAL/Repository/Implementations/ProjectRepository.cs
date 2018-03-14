using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context)
        { }

        public Task<IQueryable<Project>> GetByCompanyIdAsync(int companyId)
        {
            return Task.FromResult(
                DbSet
                    .Include(x => x.Customer)
                    .Include(x => x.Activities)
                    .Where(p => p.IsActive && p.Customer.CompanyId == companyId));
        }

        public Task<Project> GetByName(string name)
        {
            return Task.FromResult(
                DbSet
                .FirstOrDefault(p => p.IsActive && p.Name == name));
        }
    }
}
