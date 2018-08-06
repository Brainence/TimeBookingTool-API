using System;
using System.Data.Entity;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public abstract class Repository : IRepository
    {
        #region Properties

        public DbContext Context { get; set; }

        #endregion

        #region Constructors

        protected Repository(DbContext context)
        {
            Context = context ?? throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
        }

        #endregion

        #region Interface Members

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }

        #endregion
    }
}
