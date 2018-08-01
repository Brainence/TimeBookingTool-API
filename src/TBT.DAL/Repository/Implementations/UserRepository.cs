using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;
using Z.EntityFramework.Plus;

namespace TBT.DAL.Repository.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        { }

        public override Task<User> GetAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return DbSet
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.Username == email);
        }

        public Task<User> GetUserWithProjectAsync(string email)
        {
            return DbSet
                 .Include(x => x.Projects.Select(y => y.Activities))
                .FirstOrDefaultAsync(u => u.Username == email);
        }

        public async Task<bool> IsPasswordValidAsync(int userId, string password)
        {
            var user = await DbSet.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;
            return new PasswordHasher().VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Success;
        }

        public async Task ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            if (!await IsPasswordValidAsync(userId, oldPassword)) return;

            var user = DbSet.FirstOrDefault(u => u.Id == userId);
            if (user == null) return;

            user.Password = new PasswordHasher().HashPassword(newPassword);
        }

        public Task<List<User>> GetByCompanyIdAsync(int companyId)
        {
            return DbSet
                    .Include(x => x.Projects)
                    .Where(x => x.CompanyId == companyId)
                    .ToListAsync();
        }
    }
}
