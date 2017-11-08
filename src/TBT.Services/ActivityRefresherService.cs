using AutoMapper;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Business.Managers.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;
using TBT.Components.Interfaces.Logger;

namespace TBT.Services
{
    public partial class ActivityRefresherService : ServiceBase
    {
        protected TimeSpan _refresherTime;
        protected System.Timers.Timer _timer;
        protected readonly IWindsorContainer _container;
        protected SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        
        public ActivityRefresherService()
        {
            InitializeComponent();
            _container = new WindsorContainer();
            _container.Install(
                new ComponentsInstaller(),
                new ProvidersInstaller(),
                new FactoriesInstaller(),
                new ManagerInstaller(),
                new RepositoryInstaller());
            ServiceLocator.Current.SetLocatorProvider(_container);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("TBT")))
            {
                foreach (var profile in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Profile))))
                {
                    Mapper.AddProfile((Profile)Activator.CreateInstance(profile, null));
                }
            }
            _refresherTime = new TimeSpan(23, 59, 0);
            _timer = new System.Timers.Timer();
            _timer.Interval = (_refresherTime - DateTime.Now.TimeOfDay).TotalMilliseconds;
            _timer.Elapsed += TickHandler;
            _timer.Start();

        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        protected async void TickHandler(object sender, ElapsedEventArgs args)
        {
            try
            {
                await _semaphore.WaitAsync();
                if (DateTime.Now.TimeOfDay >= _refresherTime)
                {
                    var _timeEntryManager = ServiceLocator.Current.Get<ITimeEntryManager>();
                    var timeEntries = await _timeEntryManager.GetByIsRunning(true);
                    TimeEntryModel tempTimeEntry;
                    if (timeEntries.Any())
                    {
                        foreach (var item in timeEntries)
                        {
                            await _timeEntryManager.StopAsync(item.Id);
                            tempTimeEntry = new TimeEntryModel()
                            {
                                Activity = item.Activity,
                                ActivityId = item.ActivityId,
                                Comment = item.Comment,
                                Date = DateTime.Now.Date.AddDays(1),
                                TimeLimit = item.TimeLimit,
                                User = item.User,
                                UserId = item.UserId,
                                IsActive = true
                            };
                            await _timeEntryManager.StartAsync(await _timeEntryManager.AddAsync(tempTimeEntry));
                        }
                    }
                    _semaphore.Release();
                    _timer.Interval = _refresherTime.TotalMilliseconds;
                };
            }
            catch(Exception ex)
            {
                ServiceLocator.Current.Get<ILogManager>().Error(ex, $"{ex.Message} {ex.InnerException?.Message}");
            }
        }
    }
}
