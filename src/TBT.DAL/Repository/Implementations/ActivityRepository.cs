using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(DbContext context)
            : base(context)
        { }

        #region Interface Members

        public override Task<IQueryable<Activity>> GetAsync()
        {
            return Task.FromResult<IQueryable<Activity>>(
                DbSet
                .Where(x => x.IsActive)
                .Include(x => x.Project.Users)
                .OrderByDescending(a => a.Name));
        }

        public Task<IQueryable<Activity>> GetByCompanyIdAsync(int companyId)
        {
            return Task.FromResult<IQueryable<Activity>>(
                DbSet
                .Include(x => x.Project.Customer)
                .Where(x => x.IsActive && x.Project.Customer.CompanyId == companyId)
                .OrderByDescending(a => a.Name));
        }

        public Task<Activity> GetByName(string name, int projectId)
        {
            return Task.FromResult(
                DbSet
                .Where(x => x.IsActive && x.ProjectId == projectId)
                .Include(x => x.Project)
                .FirstOrDefault(p => p.Name == name));
        }

        public Task<IQueryable<Activity>> GetByProjectAsync(int id)
        {
            return Task.FromResult<IQueryable<Activity>>(
                DbSet
                .Where(x => x.IsActive && x.Project.Id == id)
                .OrderByDescending(a => a.Name));
        }

        #endregion
    }
}
