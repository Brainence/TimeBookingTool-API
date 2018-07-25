using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public class UserProjectRepository : Repository, IUserProjectRepository
    {
        public UserProjectRepository(DbContext context)
            : base(context)
        { }

        public async Task AddUserProject(int userId, int projectId)
        {
            var project = new Project() { Id = projectId };

            Context.Set<Project>().Attach(project);
            var user = Context.Set<User>().Find(userId);

            user.Projects.Add(project);
        }

        public async Task RemoveUserProject(int userId, int projectId)
        {
            var project = new Project() { Id = projectId };

            Context.Set<Project>().Attach(project);
            var user = Context.Set<User>().Include(u=>u.Projects).FirstOrDefault(u=>u.Id == userId);

            user?.Projects.Remove(project);
        }
    }
}
