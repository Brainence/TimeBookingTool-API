using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DetachAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task BulkInsertAsync(IEnumerable<TEntity> entities);
        Task<bool> ExistAsync(int id);
    }
}
