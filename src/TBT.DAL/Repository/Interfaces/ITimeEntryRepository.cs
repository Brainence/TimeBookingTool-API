using System;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface ITimeEntryRepository : IRepository, IRepository<TimeEntry>
    {
        Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, bool isRunning);
        Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, string dateString);
        Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, string fromString, string toString);
        Task<TimeSpan?> GetDurationAsync(int userId, string from, string to);
        Task<IQueryable<TimeEntry>> GetByUserFromAsync(int userId, string fromString);
        Task<IQueryable<TimeEntry>> GetByUserToAsync(int userId, string toString);
        Task<IQueryable<TimeEntry>> GetByUserAsync(int userId);
        Task<bool> StartAsync(int timeEntryId);
        Task<bool> StopAsync(int timeEntryId);
        Task<bool> RemoveAsync(int timeEntryId);
        Task<bool> UpdateAsync(TimeEntry entity, bool clientDuration);
    }
}
