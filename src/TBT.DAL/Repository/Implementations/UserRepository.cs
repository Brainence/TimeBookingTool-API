using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        { }

        public override Task<IQueryable<User>> GetAsync()
        {
            return Task.FromResult(
                DbSet
                .Where(x => x.IsActive)
                .Include(u => u.Projects.Select(p => p.Activities))
                .Include(u => u.Company)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Cast<User>());
        }
        public override Task<User> GetAsync(int id)
        {
            return Task.FromResult(
                DbSet
                .Where(u => u.IsActive && u.Id == id)
                .Include(u => u.Projects.Select(p => p.Activities))
                .Include(u => u.Company)
                .FirstOrDefault());
        }
        public Task<IQueryable<User>> GetByProjectAsync(int projectId)
        {
            return Task.FromResult(
                DbSet
                .Include(u => u.Projects.Select(p => p.Activities))
                .Include(u => u.Company)
                .Where(u => u.IsActive && u.Projects.Select(x => x.Id)
                .Contains(projectId)));
        }

        public User GetByEmail(string email)
        {
            return DbSet
                .Include(u => u.Projects.Select(p => p.Activities))
                .Include(u => u.Company)
                .FirstOrDefault(u => u.IsActive && u.Username == email);
        }

        public Task<bool> IsPasswordValid(int userId, string password)
        {
            var hasher = new PasswordHasher();
            var user = DbSet.FirstOrDefault(u => u.IsActive && u.Id == userId);

            if (user == null) return Task.FromResult(false);

            var result = hasher.VerifyHashedPassword(user.Password, password);

            return Task.FromResult(result == PasswordVerificationResult.Success);
        }

        public async Task ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var isValid = await IsPasswordValid(userId, oldPassword);
            if (!isValid) return;

            var user = DbSet.FirstOrDefault(u => u.IsActive && u.Id == userId);
            if (user == null) return;

            user.Password = new PasswordHasher().HashPassword(newPassword);
        }

        public Task<IQueryable<User>> GetByCompanyId(int companyId)
        {
            return Task.FromResult(
                DbSet
                .Include(u => u.Projects.Select(p => p.Activities))
                .Where(x => x.IsActive && x.Company.Id == companyId)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Cast<User>());
        }
    }
}
