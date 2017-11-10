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
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.Business.Managers.Implementations;
using TBT.Business.Managers.Interfaces;
using TBT.Business.Models.BusinessModels;

namespace TBT.Services
{
    public partial class ActivityRefresherService : ServiceBase
    {
        //protected DispatcherTimer _timer;
        protected TimeSpan _refresherTime;
        protected Timer _timer;
        protected readonly IWindsorContainer _container;
        protected IManagerStore _store;
        protected bool _semaphore;

        public ActivityRefresherService()
        {
            InitializeComponent();
            _semaphore = false;

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

            _store = _container.Resolve<IManagerStore>();

            _refresherTime = new TimeSpan(23, 59, 59);
            _timer = new Timer();
            _timer.Interval = 1000;
            //_timer.Interval = (_refresherTime - DateTime.Now.TimeOfDay).TotalMilliseconds;
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
            //File.AppendAllText(@"e:\123.dat", "FROM HANDLER ");
            //Debug.WriteLine(DateTime.Now);
            if (DateTime.Now.TimeOfDay >= _refresherTime && !_semaphore)
            {
                var asd = await _store.TimeEntryManager.GetAsync();
                _semaphore = true;
                var timeEntries = await _store.TimeEntryManager.GetByIsRunning(true);
                TimeEntryModel tempTimeEntry;
                if (timeEntries?.Any() == true)
                {
                    foreach (var item in timeEntries)
                    {
                        item.IsRunning = false;
                        await _store.TimeEntryManager.UpdateAsync(item);
                        tempTimeEntry = new TimeEntryModel()
                        {
                            Activity = item.Activity,
                            ActivityId = item.ActivityId,
                            Comment = item.Comment,
                            Date = DateTime.Now.Date.AddDays(1),
                            IsActive = true,
                            IsRunning = true,
                            TimeLimit = item.TimeLimit,
                            User = item.User,
                            UserId = item.UserId
                        };
                        await _store.TimeEntryManager.AddAsync(tempTimeEntry);
                    }
                }
                //_timer.Interval = (_refresherTime - DateTime.Now.TimeOfDay).TotalMilliseconds;
                _semaphore = false;
            };
        }
    }
}
