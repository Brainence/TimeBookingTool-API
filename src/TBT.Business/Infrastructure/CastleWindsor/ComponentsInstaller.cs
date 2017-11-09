using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TBT.Business.Constants;
using TBT.Business.EmailService.Interfaces;
using TBT.Business.EmailService.Models;
using TBT.Components.Implementations.ConfigurationManager;
using TBT.Components.Implementations.Logger;
using TBT.Components.Implementations.ObjectMapper;
using TBT.Components.Interfaces.ConfigurationManager;
using TBT.Components.Interfaces.Logger;
using TBT.Components.Interfaces.ObjectMapper;

namespace TBT.Business.Infrastructure.CastleWindsor
{
    public class ComponentsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IConfigurationManager>().Named(typeof(IConfigurationManager).FullName).ImplementedBy<ConfigurationManager>().LifeStyle.Transient);

            container.Register(Component.For<IObjectMapper>().Named(typeof(IObjectMapper).FullName).ImplementedBy<ObjectMapper>().LifeStyle.Transient);

            container.Register(Component.For<ILogManager>().ImplementedBy<LogManager>()
                .DependsOn(Dependency.OnAppSettingsValue("loggerName", StringConstants.LogManager)).LifeStyle.Transient);

            container.Register(Component.For<ILogManager>().ImplementedBy<LogManager>().Named("info")
                .DependsOn(Dependency.OnAppSettingsValue("loggerName", StringConstants.InformationLogManagerName)).LifeStyle.Transient);

            container.Register(Component.For<IEmailService>().ImplementedBy<EmailService.Implementations.EmailService>()
                .DependsOn(Dependency.OnValue("smtpSettings", SmtpSettingsConstants.DefaultSmtpSettings)).LifeStyle.Transient);
        }
    }
}
