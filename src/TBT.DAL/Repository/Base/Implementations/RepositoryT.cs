using EntityFramework.BulkInsert.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public abstract class Repository<TEntity> : Repository, IRepository<TEntity> where TEntity : class, IEntity
    {
        #region Properties

        protected DbSet<TEntity> DbSet { get; set; }

        #endregion

        #region Constructors

        protected Repository(DbContext context) : base(context)
        {
            DbSet = Context.Set<TEntity>();
        }

        #endregion

        #region Interface Members

        public virtual Task<IQueryable<TEntity>> GetAsync()
        {
            return Task.FromResult<IQueryable<TEntity>>(DbSet);
        }

        public virtual Task<TEntity> GetAsync(int id)
        {
            return DbSet.FindAsync(id);
        }

        public virtual Task AddAsync(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
            {
                DbEntityEntry entry = Context.Entry(entity);

                if (entry.State != EntityState.Detached)
                {
                    entry.State = EntityState.Added;
                }
                else
                {
                    DbSet.Add(entity);
                }
            });
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
            {
                DbEntityEntry entry = Context.Entry(entity);

                if (entry.State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }

                entry.State = EntityState.Modified;
            });
        }

        public virtual Task DetachAsync(TEntity entity)
        {
            return Task.Factory.StartNew(() =>
            {
                DbEntityEntry entry = Context.Entry(entity);

                entry.State = EntityState.Detached;
            });
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Task.Factory.StartNew(() =>
            {
                DbEntityEntry entry = Context.Entry(entity);

                if (entry.State != EntityState.Deleted)
                {
                    DbSet.Remove(entity);
                }
            });
        }

        public virtual async Task DeleteAsync(int id)
        {
            TEntity entity = await GetAsync(id);

            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        public virtual Task BulkInsertAsync(IEnumerable<TEntity> entities)
        {
            Context.BulkInsert(entities);

            return Task.FromResult<object>(null);
        }

        public Task<bool> ExistAsync(int id)
        {
            return Task.FromResult(DbSet.Any(i => i.Id == id));
        }

        #endregion
    }
}
