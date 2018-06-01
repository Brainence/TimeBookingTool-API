﻿using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IAccountRepository : IRepository, IRepository<User>
    {
        User GetByEmail(string email);
        Task<IQueryable<User>> GetByProjectAsync(int projectId);
    }
}