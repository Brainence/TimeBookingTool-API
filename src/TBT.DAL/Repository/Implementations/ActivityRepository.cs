using System.Collections.Generic;
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

        public override Task<List<Activity>> GetAsync()
        {
            return DbSet.Include(x => x.Project.Users).ToListAsync();
        }

        public Task<List<Activity>> GetByCompanyIdAsync(int companyId)
        {
            return
                DbSet
                .Include(x => x.Project.Customer)
                .Where(x => x.Project.Customer.CompanyId == companyId).ToListAsync();
        }

        public Task<List<Activity>> GetByProjectIdAsync(int projectId)
        {
            return DbSet.Where(x => x.ProjectId == projectId).ToListAsync();
        }

        public Task<Activity> GetByNameAsync(string name, int projectId)
        {
            return DbSet.FirstOrDefaultAsync(x => x.ProjectId == projectId && x.Name == name);
        }

        #endregion
    }
}
