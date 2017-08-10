using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TBT.Business.Providers.Implementations;
using TBT.Business.Providers.Interfaces;

namespace TBT.Business.Infrastructure.CastleWindsor
{
    public class ProvidersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IConfigurationProvider>().ImplementedBy<ConfigurationProvider>().LifeStyle.Transient);
        }
    }
}
