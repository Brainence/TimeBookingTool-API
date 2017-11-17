using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Data.Entity;
using TBT.Business.Constants;
using TBT.Business.Infrastructure.CastleWindsor.ComponentSelector;
using TBT.DAL.Repository;
using TBT.DAL.Repository.Interfaces;
using TBT.DAL.Repository.Implementations;

namespace TBT.Business.Infrastructure.CastleWindsor
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IApplicationUnitOfWork>().Named(typeof(IApplicationUnitOfWork).FullName).ImplementedBy<ApplicationUnitOfWork>().LifeStyle.Transient);

            container.Register(Component.For<DbContext>().ImplementedBy<DataContext>()
                .DependsOn(Dependency.OnAppSettingsValue("connectionString", StringConstants.ConnectionString)).LifeStyle.Transient);

            container.Register(Component.For<ICustomerRepository>().Named(typeof(ICustomerRepository).FullName).ImplementedBy<CustomerRepository>().LifeStyle.Transient);
            container.Register(Component.For<IActivityRepository>().Named(typeof(IActivityRepository).FullName).ImplementedBy<ActivityRepository>().LifeStyle.Transient);
            container.Register(Component.For<IProjectRepository>().Named(typeof(IProjectRepository).FullName).ImplementedBy<ProjectRepository>().LifeStyle.Transient);
            container.Register(Component.For<IUserRepository>().Named(typeof(IUserRepository).FullName).ImplementedBy<UserRepository>().LifeStyle.Transient);
            container.Register(Component.For<ITimeEntryRepository>().Named(typeof(ITimeEntryRepository).FullName).ImplementedBy<TimeEntryRepository>().LifeStyle.Transient);
            container.Register(Component.For<ICompanyRepository>().Named(typeof(ICompanyRepository).FullName).ImplementedBy<CompanyRepository>().LifeStyle.Transient);
        }
    }
}
