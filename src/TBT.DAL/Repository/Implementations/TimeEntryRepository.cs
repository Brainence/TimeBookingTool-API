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

        public async override Task<TimeEntry> GetAsync(int id)
        {
            var timeEntry = DbSet
                .Where(x => x.IsActive)
                .Include(x => x.Activity)
                .Include(x => x.User)
                .Include(x => x.Activity.Project.Customer)
                .FirstOrDefault(x => x.Id == id);

            if (timeEntry.IsRunning && timeEntry.IsActive && timeEntry.LastUpdated.HasValue)
            {
                var totalDays = (DateTime.UtcNow.Date - timeEntry.Date.Date).Duration().TotalDays;
                var n = (int)Math.Floor(totalDays);

                if (n == 0)
                {
                    timeEntry.Duration += (DateTime.UtcNow - timeEntry.LastUpdated.Value).Duration();
                    if (timeEntry.Duration >= _dayLimit)
                    {
                        timeEntry.Duration = _dayLimit;
                        timeEntry.IsRunning = false;
                    }
                    timeEntry.LastUpdated = DateTime.UtcNow;
                }
                //else if (timeEntry.User != null && timeEntry.User.CurrentTimeZone.HasValue)
                //{
                //    var y = timeEntry.LastUpdated.Value.Date;
                //    var x = new DateTime(y.Year, y.Month, y.Day + 1)
                //        .Subtract(timeEntry.User.CurrentTimeZone.Value)
                //        .Subtract(timeEntry.LastUpdated.Value)
                //        .Duration();

                //    timeEntry.Duration = (timeEntry.Duration + x).Duration();
                //    timeEntry.IsRunning = false;

                //    await Context.SaveChangesAsync();
                //}
            }

            return await Task.FromResult(timeEntry);
        }

        private void CheckTimeEntry(TimeEntry timeEntry)
        {
            if (timeEntry.IsRunning && timeEntry.IsActive && timeEntry.LastUpdated.HasValue)
            {
                var totalDays = (DateTime.UtcNow.Date - timeEntry.Date.Date).Duration().TotalDays;
                var n = (int)Math.Floor(totalDays);

                if (n == 0)
                {
                    timeEntry.Duration += (DateTime.UtcNow - timeEntry.LastUpdated.Value).Duration();
                    if (timeEntry.Duration >= _dayLimit)
                    {
                        timeEntry.Duration = _dayLimit;
                        timeEntry.IsRunning = false;
                    }
                    timeEntry.LastUpdated = DateTime.UtcNow;
                }
                //else if (timeEntry.User != null && timeEntry.User.CurrentTimeZone.HasValue)
                //{
                //    var y = timeEntry.LastUpdated.Value.Date;
                //    var x = new DateTime(y.Year, y.Month, y.Day + 1)
                //        .Subtract(timeEntry.User.CurrentTimeZone.Value)
                //        .Subtract(timeEntry.LastUpdated.Value)
                //        .Duration();

                //    timeEntry.Duration = (timeEntry.Duration + x).Duration();
                //    timeEntry.IsRunning = false;
                //}
            }
        }

        public async Task<IQueryable<TimeEntry>> GetByUserAsync(int userId)
        {
            var timeEntries = DbSet
                .Where(t => t.User.Id == userId
                            && t.IsActive)
                .Include(x => x.Activity.Project.Customer)
                .OrderBy(t => t.Date)
                .Cast<TimeEntry>();

            foreach (var timeEntry in timeEntries.Where(t => t.IsRunning))
            {
                CheckTimeEntry(timeEntry);
            }
            await Context.SaveChangesAsync();

            return await Task.FromResult(timeEntries);
        }

        public async Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, bool isRunning)
        {
            var timeEntries = DbSet
                .Where(t => t.User.Id == userId
                            && t.IsRunning == isRunning
                            && t.IsActive)
                .Include(x => x.Activity.Project.Customer)
                .Include(x => x.User)
                .OrderBy(t => t.Date)
                .Cast<TimeEntry>();

            foreach (var timeEntry in timeEntries.Where(t => t.IsRunning))
            {
                CheckTimeEntry(timeEntry);
            }
            await Context.SaveChangesAsync();

            return await Task.FromResult(timeEntries);
        }

        public async Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, string dateString)
        {
            try
            {
                var date = ParseDate(dateString);
                var nextDay = date.AddDays(1);

                var timeEntries = DbSet
                    .Where(t => t.User.Id == userId
                                && t.Date >= date
                                && t.Date < nextDay
                                && t.IsActive)
                    .Include(x => x.Activity.Project.Customer)
                    .Include(x => x.User.Projects)
                    .Include(x => x.User.TimeEntries)
                    .OrderBy(t => t.Date)
                    .Cast<TimeEntry>();

                foreach (var timeEntry in timeEntries.Where(t => t.IsRunning))
                {
                    CheckTimeEntry(timeEntry);
                }
                await Context.SaveChangesAsync();

                return await Task.FromResult(timeEntries);
            }
            catch { return null; }
        }

        public async Task<IQueryable<TimeEntry>> GetByUserAsync(int userId, string fromString, string toString)
        {
            try
            {
                var from = ParseDate(fromString);
                var to = ParseDate(toString);

                to = to.AddDays(1);


                var timeEntries = DbSet
                    .Where(t => t.User.Id == userId
                                && t.Date >= from
                                && t.Date < to
                                && t.IsActive)
                    .Include(x => x.Activity.Project.Customer)
                    .Include(x => x.User.Projects)
                    .Include(x => x.User.TimeEntries)
                    .OrderBy(t => t.Date)
                    .Cast<TimeEntry>();

                foreach (var timeEntry in timeEntries.Where(t => t.IsRunning))
                {
                    CheckTimeEntry(timeEntry);
                }
                await Context.SaveChangesAsync();

                return await Task.FromResult(timeEntries);
            }
            catch { return null; }
        }

        public async Task<IQueryable<TimeEntry>> GetByUserFromAsync(int userId, string fromString)
        {
            try
            {
                var from = ParseDate(fromString);

                var timeEntries = DbSet
                    .Where(t => t.User.Id == userId
                                && t.Date >= from
                                && t.IsActive)
                    .Include(x => x.Activity.Project.Customer)
                    .Include(x => x.User.Projects)
                    .Include(x => x.User.TimeEntries)
                    .OrderBy(t => t.Date)
                    .Cast<TimeEntry>();

                foreach (var timeEntry in timeEntries.Where(t => t.IsRunning))
                {
                    CheckTimeEntry(timeEntry);
                }
                await Context.SaveChangesAsync();

                return await Task.FromResult(timeEntries);
            }
            catch { return null; }
        }

        public async Task<IQueryable<TimeEntry>> GetByUserToAsync(int userId, string toString)
        {
            try
            {
                var to = ParseDate(toString);
                to = to.AddDays(1);

                var timeEntries = DbSet
                    .Where(t => t.User.Id == userId
                                && t.Date < to
                                && t.IsActive)
                    .Include(x => x.Activity.Project.Customer)
                    .Include(x => x.User.Projects)
                    .Include(x => x.User.TimeEntries)
                    .OrderBy(t => t.Date)
                    .Cast<TimeEntry>();

                foreach (var timeEntry in timeEntries.Where(t => t.IsRunning))
                {
                    CheckTimeEntry(timeEntry);
                }
                await Context.SaveChangesAsync();

                return await Task.FromResult(timeEntries);
            }
            catch { return null; }
        }

        public async Task<bool> StartAsync(int timeEntryId)
        {
            try
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
                    var canStart = res.HasValue ? res.Value.TotalHours < timeEntry.User.TimeLimit.Value : true;

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
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> StopAsync(int timeEntryId)
        {
            try
            {
                var timeEntry = DbSet.FirstOrDefault(t => t.Id == timeEntryId);
                if (timeEntry == null) return await Task.FromResult(false);

                if (!timeEntry.IsRunning) return await Task.FromResult(false);
                if (!timeEntry.LastUpdated.HasValue) return await Task.FromResult(false);

                timeEntry.IsRunning = false;
                timeEntry.Duration += DateTime.UtcNow - timeEntry.LastUpdated.Value;
                if (timeEntry.Duration >= _dayLimit) timeEntry.Duration = _dayLimit;

                await Context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> RemoveAsync(int timeEntryId)
        {
            try
            {
                var timeEntry = DbSet.FirstOrDefault(t => t.Id == timeEntryId);
                if (timeEntry == null) return await Task.FromResult(false);

                timeEntry.IsRunning = false;
                timeEntry.IsActive = false;

                await Context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateAsync(TimeEntry entity, bool clientDuration)
        {
            try
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
                timeEntry.TimeLimit = entity.TimeLimit;

                await Context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<TimeSpan?> GetDurationAsync(int userId, string fromString, string toString)
        {
            try
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

                TimeSpan? res = timeEntries.Count > 0 ? timeEntries.Select(t => t.Duration).Aggregate((t1, t2) => t1.Add(t2)) : (TimeSpan?)null;

                return await Task.FromResult(res);
            }
            catch { return null; }
        }

        public async Task<IQueryable<TimeEntry>> GetByIsRunning(bool isRunning)
        {
            try
            {
                var timeEntries = DbSet
                    .Where(t => t.IsRunning == isRunning
                                && t.IsActive)
                    .Include(x => x.Activity.Project.Customer)
                    .Include(x => x.User.Projects)
                    .Include(x => x.User.TimeEntries)
                    .OrderBy(t => t.Date)
                    .Cast<TimeEntry>();

                return await Task.FromResult(timeEntries);
            }
            catch { return null; }
        }
    }
}
