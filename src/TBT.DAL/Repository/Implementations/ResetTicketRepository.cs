using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public class ResetTicketRepository : Repository<ResetTicket>, IResetTicketRepository
    {
        private static RNGCryptoServiceProvider _cryptoService;

        public ResetTicketRepository(DbContext context)
            : base(context)
        {
            _cryptoService = new RNGCryptoServiceProvider();
        }

        public async Task<ResetTicket> CreateResetTicketAsync(int userId)
        {
            var user = Context.Set<User>().FirstOrDefault(u => u.Id == userId);
            if (user == null) return null;
            var resetTicket = new ResetTicket()
            {
                ExpirationDate = DateTime.UtcNow.AddHours(1),
                IsUsed = false,
                Username = user.Username,
                Token = RandomString(64)
            };
            resetTicket = DbSet.Add(resetTicket);
            await Context.SaveChangesAsync();
            return resetTicket;
        }

        static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (_cryptoService = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];
                while (length-- > 0)
                {
                    _cryptoService.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }
            return res.ToString();
        }

        public async Task<bool> ChangePasswordAsync(int userId, string newPassword, string token)
        {
            var user = Context.Set<User>().FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            var resetTicket = DbSet.FirstOrDefault(rt => rt.Username == user.Username && rt.Token == token && !rt.IsUsed);
            if (resetTicket == null) return false;
            if (resetTicket.ExpirationDate < DateTime.UtcNow) return false;

            user.Password = newPassword;
            resetTicket.IsUsed = true;

            DbEntityEntry entry = Context.Entry(user);
            if (entry.State == EntityState.Detached)
            {
                Context.Set<User>().Attach(user);
            }
            entry.State = EntityState.Modified;
            entry = Context.Entry(user);
            if (entry.State == EntityState.Detached)
            {
                Context.Set<User>().Attach(user);
            }
            entry.State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
