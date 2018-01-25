using Quartz;
using Quartz.Impl;
using TBT.Api.Common.Quartz.Jobs;

namespace TBT.Api.Common.Quartz.Schedulers
{
    public class GlobalSchedular
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            var trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule(s => s.OnEveryDay()
                                                     .StartingDailyAt(new TimeOfDay(23, 59, 59)))
                .Build();
            scheduler.ScheduleJob(JobBuilder.Create<RefreshTimeEntriesJob>().Build(), trigger);
        }
    }
}