﻿using Microsoft.AspNet.Identity;
using System.Data.Entity;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        public override void InitializeDatabase(DataContext context)
        {
            context.Companies.Add(new Company()
            {
                CompanyName = "Brainence",
                IsActive = true,
            });

            context.Users.Add(new User()
            {
                FirstName = "Sergey",
                LastName = "Chujko",
                Password = new PasswordHasher().HashPassword("brainence!"),
                Username = "schuiko@brainence.com",
                IsAdmin = true,
                IsActive = true,
                CompanyId = 1
            });            

            base.InitializeDatabase(context);
        }
    }
}
