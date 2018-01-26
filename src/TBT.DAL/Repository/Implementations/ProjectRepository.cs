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

        public override Task<IQueryable<Project>> GetAsync()
        {
            return Task.FromResult(
                DbSet
                .Where(p => p.IsActive)
                .Include(x => x.Users)
                .Include(x => x.Customer));
        }
        public Task<IQueryable<Project>> GetByActivityAsync(int activityId)
        {
            return Task.FromResult(
                DbSet
                .Where(p => p.Activities.Select(x => x.Id).Contains(activityId))
                .Include(u => u.Users.Select(p => p.Projects))
                .Include(u => u.Activities.Select(t => t.Project))
                .Include(x => x.Customer));
        }

        public Task<IEnumerable<Project>> GetByCompanyIdAsync(int companyId)
        {
            return Task.FromResult(
                DbSet
                    .Include(x => x.Users)
                    .Include(x => x.Customer)
                    .Where(p => p.IsActive && p.Customer.CompanyId == companyId)
                    .AsEnumerable());
        }

        public Task<IQueryable<Project>> GetByCustomerAsync(int customerId)
        {
            return Task.FromResult(
                DbSet
                .Where(p => p.IsActive && p.CustomerId == customerId));
        }

        public Task<Project> GetByName(string name)
        {
            return Task.FromResult(
                DbSet
                .Where(p => p.IsActive)
                .Include(u => u.Activities)
                .Include(x => x.Customer)
                .FirstOrDefault(p => p.Name == name));
        }

        public Task<IQueryable<Project>> GetByUserAsync(int userId)
        {
            return Task.FromResult(
                DbSet
                .Include(u => u.Users.Select(p => p.Projects))
                .Include(u => u.Activities.Select(t => t.Project))
                .Include(x => x.Customer)
                .Where(p => p.IsActive && p.Users.Select(x => x.Id).Contains(userId)));
        }
    }
}
