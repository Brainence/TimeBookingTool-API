using System.Data.Entity;

namespace TBT.DAL.Repository.Interfaces
{
    public interface IRepository
    {
        DbContext Context { get; set; }
    }
}
