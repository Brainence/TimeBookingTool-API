using System;
using Hangfire;
using Microsoft.Owin;
using Owin;
using TBT.Api.Common.Quartz.Jobs;

[assembly: OwinStartup(typeof(TBT.WebApi.Startup))]

namespace TBT.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("TimeBookingToolConnectionString");
            //app.UseHangfireDashboard();
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate(() => new RefreshTimeEntriesJob().Check(), Cron.Daily, TimeZoneInfo.Local);
        }
    }
}