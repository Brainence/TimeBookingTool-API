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

        public override Task<User> GetAsync(int id)
        {
            return Task.FromResult(
                DbSet.FirstOrDefault(u => u.IsActive && u.Id == id));
        }

        public User GetByEmail(string email)
        {
            return DbSet.Include(u => u.Company)
                .FirstOrDefault(u => u.IsActive && u.Username == email);
        }

        public User GetUserWithProject(string email)
        {
            return DbSet
                 .Include(x => x.Projects.Select(y => y.Activities))
                .FirstOrDefault(u => u.IsActive && u.Username == email);
        }

        public Task<bool> IsPasswordValid(int userId, string password)
        {
            var user = DbSet.FirstOrDefault(u => u.IsActive && u.Id == userId);

            if (user == null) return Task.FromResult(false);

            var result = new PasswordHasher().VerifyHashedPassword(user.Password, password);

            return Task.FromResult(result == PasswordVerificationResult.Success);
        }

        public async Task ChangePassword(int userId, string oldPassword, string newPassword)
        {
            if (!await IsPasswordValid(userId, oldPassword)) return;

            var user = DbSet.FirstOrDefault(u => u.IsActive && u.Id == userId);
            if (user == null) return;

            user.Password = new PasswordHasher().HashPassword(newPassword);
        }

        public Task<IQueryable<User>> GetByCompanyId(int companyId)
        {
            return Task.FromResult(
                DbSet
                .Include(x => x.Projects)
                .Where(x => x.IsActive && x.CompanyId == companyId)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Cast<User>());
        }
    }
}
