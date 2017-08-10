using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TBT.Business.EmailService.Interfaces;
using TBT.Business.Managers.Implementations;
using TBT.Business.Managers.Interfaces;

namespace TBT.Business.Infrastructure.CastleWindsor
{
    public class ManagerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IManagerStore>().ImplementedBy<ManagerStore>().LifeStyle.Transient);

            container.Register(Component.For<IAccountManager>().Named(typeof(IAccountManager).FullName).ImplementedBy<AccountManager>().LifeStyle.Transient);
            container.Register(Component.For<IProjectManager>().Named(typeof(IProjectManager).FullName).ImplementedBy<ProjectManager>().LifeStyle.Transient);
            container.Register(Component.For<ICustomerManager>().Named(typeof(ICustomerManager).FullName).ImplementedBy<CustomerManager>().LifeStyle.Transient);
            container.Register(Component.For<IActivityManager>().Named(typeof(IActivityManager).FullName).ImplementedBy<ActivityManager>().LifeStyle.Transient);
            container.Register(Component.For<IUserManager>().Named(typeof(IUserManager).FullName).ImplementedBy<UserManager>().LifeStyle.Transient);
            container.Register(Component.For<ITimeEntryManager>().Named(typeof(ITimeEntryManager).FullName).ImplementedBy<TimeEntryManager>().LifeStyle.Transient);
            container.Register(Component.For<IUserProjectManager>().Named(typeof(IUserProjectManager).FullName).ImplementedBy<UserProjectManager>().LifeStyle.Transient);
            container.Register(Component.For<IResetTicketManager>().Named(typeof(IResetTicketManager).FullName).ImplementedBy<ResetTicketManager>().LifeStyle.Transient);
        }
    }
}
