using Microsoft.AspNet.Identity;
using System.Data.Entity;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        public override void InitializeDatabase(DataContext context)
        {
            context.Users.Add(new User()
            {
                FirstName = "Vasyl",
                LastName = "Malanii",
                Password = new PasswordHasher().HashPassword("brainence!"),
                Username = "vmalanii@brainence.com",
                IsAdmin = true,
                IsActive = true
            });

            base.InitializeDatabase(context);
        }
    }
}
