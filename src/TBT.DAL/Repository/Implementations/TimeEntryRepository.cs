using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using TBT.DAL.Entities;
using TBT.DAL.Repository.Interfaces;

namespace TBT.DAL.Repository.Implementations
{
    public class TimeEntryRepository : Repository<TimeEntry>, ITimeEntryRepository
    {
        private readonly TimeSpan _dayLimit = new TimeSpan(23, 59, 59);

        public TimeEntryRepository(DbContext context) : base(context) { }

        private async Task CheckTimeEntryAsync(TimeEntry timeEntry, bool needSave = true)
        {
            if (timeEntry != null && timeEntry.IsRunning && timeEntry.LastUpdated.HasValue)
            {
                if ((int)Math.Floor((DateTime.UtcNow.Date - timeEntry.Date.Date).Duration().TotalDays) == 0)
                {
                    timeEntry.Duration += (DateTime.UtcNow - timeEntry.LastUpdated.Value).Duration();
                    if (timeEntry.Duration >= _dayLimit)
                    {
                        timeEntry.Duration = _dayLimit;
                        timeEntry.IsRunning = false;
                    }
                    timeEntry.LastUpdated = DateTime.UtcNow;
                    if (needSave)
                    {
                        await Context.SaveChangesAsync();
                    }
                }
            }
        }

        private async Task CheckTimeEntryAsync(IEnumerable<TimeEntry> timeEntries)
        {
            foreach (var timeEntry in timeEntries.Where(x => x.IsRunning))
            {
                await CheckTimeEntryAsync(timeEntry, false);
            }
            await Context.SaveChangesAsync();
        }

        public override async Task<TimeEntry> GetAsync(int id)
        {
            var timeEntry = DbSet
                .Include(x => x.Activity.Project.Customer)
                .FirstOrDefault(x => x.Id == id);
            await CheckTimeEntryAsync(timeEntry);
            return timeEntry;
        }

        public async Task<List<TimeEntry>> GetByUserAsync(int userId)
        {
            var timeEntries = await DbSet
                    .Include(x => x.Activity.Project.Customer)
                    .Where(t => t.UserId == userId)
                    .ToListAsync();
            await CheckTimeEntryAsync(timeEntries);
            return timeEntries;
        }

        public async Task<List<TimeEntry>> GetByUserAsync(int userId, bool isRunning)
        {
            var timeEntries = await DbSet
                    .Include(x => x.Activity.Project.Customer)
                    .Where(t => t.UserId == userId && t.IsRunning == isRunning)
                    .ToListAsync();
            await CheckTimeEntryAsync(timeEntries);
            return timeEntries;
        }

        public async Task<List<TimeEntry>> GetByUserAsync(int userId, DateTime date)
        {
            var nextDay = date.AddDays(1);
            var timeEntries = await DbSet
                    .Include(x => x.Activity.Project.Customer)
                    .Where(t => t.UserId == userId && t.Date >= date && t.Date < nextDay)
                    .ToListAsync();
            await CheckTimeEntryAsync(timeEntries);
            return timeEntries;
        }

        public async Task<List<TimeEntry>> GetByUserAsync(int userId, DateTime from, DateTime to, bool needRunning)
        {
            from = from.ToUniversalTime();
            to = to.ToUniversalTime().AddDays(1);
            var query =  DbSet
                    .Include(x => x.Activity.Project.Customer)
                    .Include(x => x.User)
                    .Where(t => t.UserId == userId && t.Date >= from && t.Date < to);

            if (!needRunning)
            {
                query = query.Where(x => !x.IsRunning);
            }

            var timeEntries = await query.ToListAsync();
            await CheckTimeEntryAsync(timeEntries);
            return timeEntries;
        }

        public async Task<bool> StartAsync(int timeEntryId)
        {
            var timeEntry = DbSet.Include(x => x.User).FirstOrDefault(x => x.Id == timeEntryId);
            if (timeEntry == null) return false;
            var timeEntries = DbSet.Where(x => x.UserId == timeEntry.UserId);

            var running = timeEntries.Where(x => x.IsRunning);
            if (running.Any())
            {
                foreach (var temp in running)
                {
                    if (!temp.LastUpdated.HasValue) continue;
                    temp.IsRunning = false;
                    temp.Duration += DateTime.UtcNow - temp.LastUpdated.Value;
                    if (temp.Duration >= _dayLimit) temp.Duration = _dayLimit;
                }
                await Context.SaveChangesAsync();
            }


            if (timeEntry.User.TimeLimit > 0)
            {
                var utcNow = DateTime.UtcNow;
                var from = new DateTime(utcNow.Year, utcNow.Month, 1);
                var to = new DateTime(utcNow.Year, utcNow.Month, DateTime.DaysInMonth(utcNow.Year, utcNow.Month), 23, 59, 59);

                var sum = timeEntries.Where(t => t.Date >= from && t.Date <= to && !t.IsRunning).ToList().Aggregate(TimeSpan.Zero, (s, entry) => s.Add(entry.Duration));

                if (sum.TotalHours > timeEntry.User.TimeLimit)
                {
                    return false;
                }
            }
            timeEntry.IsRunning = true;
            timeEntry.LastUpdated = DateTime.UtcNow;
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> StopAsync(int timeEntryId)
        {
            var timeEntry = DbSet.FirstOrDefault(t => t.Id == timeEntryId);
            if (timeEntry == null || !timeEntry.IsRunning || !timeEntry.LastUpdated.HasValue) return false;

            timeEntry.IsRunning = false;
            timeEntry.Duration += DateTime.UtcNow - timeEntry.LastUpdated.Value;
            if (timeEntry.Duration >= _dayLimit)
            {
                timeEntry.Duration = _dayLimit;
            }
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(int timeEntryId)
        {
            var timeEntry = DbSet.FirstOrDefault(t => t.Id == timeEntryId);
            if (timeEntry == null) return false;

            timeEntry.IsRunning = false;
            timeEntry.IsActive = false;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(TimeEntry entity, bool clientDuration)
        {
            if (entity == null) return false;

            var timeEntry = DbSet.FirstOrDefault(t => t.Id == entity.Id);
            if (timeEntry == null) return false;

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
            {
                timeEntry.IsRunning = entity.IsRunning;
            }
            timeEntry.LastUpdated = DateTime.UtcNow;
            timeEntry.Comment = entity.Comment;
            timeEntry.IsActive = entity.IsActive;

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<TimeSpan> GetDurationAsync(int userId, DateTime from, DateTime to)
        {
            from = from.ToUniversalTime();
            to = to.ToUniversalTime().AddDays(1);
            var timeEntries = await DbSet
                .Where(t => t.UserId == userId && t.Date >= from && t.Date < to && !t.IsRunning)
                .ToListAsync();
            return timeEntries.Aggregate(TimeSpan.Zero, (t1, t2) => t1.Add(t2.Duration));
        }

        public Task<List<TimeEntry>> GetByIsRunningAsync(bool isRunning)
        {
            return DbSet
                .Include(x => x.Activity)
                .Include(x => x.User)
                .Where(t => t.IsRunning == isRunning)
                .ToListAsync();
        }
    }
}
