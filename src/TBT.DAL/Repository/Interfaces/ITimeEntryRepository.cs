using System;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;

namespace TBT.DAL.Repository.Interfaces
{
    public interface ITimeEntryRepository : IRepository, IRepository<TimeEntry>
    {
        Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, bool isRunning);
        Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, DateTime date);
        Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, DateTime from, DateTime to, bool running);
        Task<TimeSpan> GetDurationAsync(int userId, DateTime from, DateTime to);
        Task<IQueryable<TimeEntry>> GetByUserAsync(int userId);
        Task<bool> StartAsync(int timeEntryId);
        Task<bool> StopAsync(int timeEntryId);
        Task<bool> RemoveAsync(int timeEntryId);
        Task<bool> UpdateAsync(TimeEntry entity, bool clientDuration);
        Task<IQueryable<TimeEntry>> GetByIsRunning(bool isRunning);
    }
}
