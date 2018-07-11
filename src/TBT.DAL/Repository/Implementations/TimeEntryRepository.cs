using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public class TimeEntryRepository : Repository<TimeEntry>, ITimeEntryRepository
    {
        private TimeSpan _dayLimit;
        public TimeEntryRepository(DbContext context)
            : base(context)
        {
            _dayLimit = new TimeSpan(23, 59, 59);
        }

        private static DateTime ParseDate(string urlSafeDateString)
        {
            DateTime date = DateTime.ParseExact(urlSafeDateString, "yyyyMMddTHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            return date;
        }

        public override Task<TimeEntry> GetAsync(int id)
        {
            var timeEntry = DbSet
                .Include(x => x.Activity.Project.Customer)
                .FirstOrDefault(x => x.Id == id && x.IsActive);

            CheckTimeEntry(timeEntry);

            return Task.FromResult(timeEntry);
        }

        private void CheckTimeEntry(TimeEntry timeEntry)
        {
            if (timeEntry != null && timeEntry.IsRunning && timeEntry.IsActive && timeEntry.LastUpdated.HasValue)
            {
                var totalDays = (int)Math.Floor((DateTime.UtcNow.Date - timeEntry.Date.Date).Duration().TotalDays);

                if (totalDays == 0)
                {
                    timeEntry.Duration += (DateTime.UtcNow - timeEntry.LastUpdated.Value).Duration();
                    if (timeEntry.Duration >= _dayLimit)
                    {
                        timeEntry.Duration = _dayLimit;
                        timeEntry.IsRunning = false;
                    }
                    timeEntry.LastUpdated = DateTime.UtcNow;
                }
            }
        }

        public async Task<IQueryable<TimeEntry>> GetByUserAsync(int userId)
        {
            var timeEntries = DbSet
                .Where(t => t.UserId == userId
                            && t.IsActive)
                .Include(x => x.Activity.Project.Customer)
                .OrderBy(t => t.Date);

            foreach (var timeEntry in timeEntries.Where(t => t.IsRunning).ToList())
            {
                CheckTimeEntry(timeEntry);
            }
            await Context.SaveChangesAsync();

            return timeEntries;
        }

        public async Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, bool isRunning)
        {
            var timeEntries = DbSet
                .Where(t => t.UserId == userId
                            && t.IsRunning == isRunning
                            && t.IsActive)
                .Include(x => x.Activity.Project.Customer)
                .OrderBy(t => t.Date);

            foreach (var timeEntry in timeEntries.Where(t => t.IsRunning))
            {
                CheckTimeEntry(timeEntry);
            }
            await Context.SaveChangesAsync();

            return await Task.FromResult(timeEntries);
        }

        public async Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, string dateString)
        {
            var date = ParseDate(dateString);
            var nextDay = date.AddDays(1);

            var timeEntries = DbSet
                .Where(t => t.UserId == userId
                            && t.Date >= date
                            && t.Date < nextDay
                            && t.IsActive)
                .Include(x => x.Activity.Project.Customer)
                .OrderBy(t => t.Date);

            foreach (var timeEntry in timeEntries.Where(t => t.IsRunning))
            {
                CheckTimeEntry(timeEntry);
            }
            await Context.SaveChangesAsync();

            return await Task.FromResult(timeEntries);
        }

        public async Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, string fromString, string toString,bool needRunning)
        {
            var from = ParseDate(fromString);
            var to = ParseDate(toString);

            to = to.AddDays(1);


            var timeEntries = DbSet
                .Where(t => t.UserId == userId
                            && t.Date >= from
                            && t.Date < to
                            && t.IsActive)
                .Include(x => x.Activity.Project.Customer)
                .Include(x => x.User)
                .OrderBy(t => t.Date);

            foreach (var timeEntry in timeEntries.Where(t => t.IsRunning))
            {
                CheckTimeEntry(timeEntry);
            }
            await Context.SaveChangesAsync();

            return await Task.FromResult(needRunning ? timeEntries: timeEntries.Where(x => !x.IsRunning));
        }

      

        public async Task<bool> StartAsync(int timeEntryId)
        {
            var timeEntry = DbSet.FirstOrDefault(t => t.Id == timeEntryId);
            if (timeEntry == null) return await Task.FromResult(false);

            if (timeEntry.User != null)
            {
                var utcNow = DateTime.UtcNow;

                var from = new DateTime(utcNow.Year, utcNow.Month, 1);
                var to = new DateTime(utcNow.Year, utcNow.Month, DateTime.DaysInMonth(utcNow.Year, utcNow.Month));

                to = to.AddDays(1);

                var timeEntries = DbSet
                    .Where(t => t.User.Id == timeEntry.User.Id
                                && t.Date >= from
                                && t.Date < to
                                && t.IsActive
                                && !t.IsRunning)
                    .ToList();

                var res = timeEntries.Count > 0 ? timeEntries.Select(t => t.Duration).Aggregate((t1, t2) => t1.Add(t2)) : (TimeSpan?)null;
                var canStart = res.HasValue ? res.Value.TotalHours < (timeEntry.User.TimeLimit.HasValue ? timeEntry.User.TimeLimit : 0) : true;

                if (!canStart) await Task.FromResult(false);
            }

            var runningTimeEntires = DbSet.Where(t => t.User.Id == timeEntry.UserId && t.IsRunning == true && t.IsActive);
            foreach (var t in runningTimeEntires)
            {
                if (!t.LastUpdated.HasValue) continue;
                t.IsRunning = false;
                t.Duration += DateTime.UtcNow - t.LastUpdated.Value;
                if (t.Duration >= _dayLimit) t.Duration = _dayLimit;
            }

            timeEntry.IsRunning = true;
            timeEntry.LastUpdated = DateTime.UtcNow;

            await Context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> StopAsync(int timeEntryId)
        {
            var timeEntry = DbSet.FirstOrDefault(t => t.Id == timeEntryId);
            if (timeEntry == null) return false;

            if (!timeEntry.IsRunning) return false;
            if (!timeEntry.LastUpdated.HasValue) return false;

            timeEntry.IsRunning = false;
            timeEntry.Duration += DateTime.UtcNow - timeEntry.LastUpdated.Value;
            if (timeEntry.Duration >= _dayLimit) timeEntry.Duration = _dayLimit;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(int timeEntryId)
        {
            var timeEntry = DbSet.FirstOrDefault(t => t.Id == timeEntryId);
            if (timeEntry == null) return await Task.FromResult(false);

            timeEntry.IsRunning = false;
            timeEntry.IsActive = false;

            await Context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(TimeEntry entity, bool clientDuration)
        {
            if (entity == null) return await Task.FromResult(false);

            var timeEntry = DbSet.FirstOrDefault(t => t.Id == entity.Id);
            if (timeEntry == null) return await Task.FromResult(false);

            timeEntry.Duration = clientDuration
                ? entity.Duration
                : timeEntry.IsRunning
                    ? timeEntry.Duration + (DateTime.UtcNow - timeEntry.LastUpdated.Value)
                    : timeEntry.Duration;

            if (timeEntry.Duration >= _dayLimit)
            {
                timeEntry.Duration = _dayLimit;
                timeEntry.IsRunning = false;
            }
            else
                timeEntry.IsRunning = entity.IsRunning;

            timeEntry.LastUpdated = DateTime.UtcNow;
            timeEntry.Comment = entity.Comment;
            timeEntry.IsActive = entity.IsActive;
            
            await Context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<TimeSpan> GetDurationAsync(int userId, string fromString, string toString)
        {
            var from = ParseDate(fromString).ToUniversalTime();
            var to = ParseDate(toString).ToUniversalTime();

            to = to.AddDays(1);

            var timeEntries = DbSet
                .Where(t => t.User.Id == userId
                            && t.Date >= from
                            && t.Date < to
                            && t.IsActive
                            && !t.IsRunning)
                .ToList();

           return await Task.FromResult(timeEntries.Any() ? timeEntries.Select(t => t.Duration).Aggregate((t1, t2) => t1.Add(t2)) : TimeSpan.Zero);

        }

        public Task<IQueryable<TimeEntry>> GetByIsRunning(bool isRunning)
        {
            return Task.FromResult(DbSet
                .Where(t => t.IsRunning == isRunning
                            && t.IsActive)
                .Include(x => x.Activity)
                .Include(x => x.User));
        }
    }
}
