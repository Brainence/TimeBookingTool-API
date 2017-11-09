using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using TBT.Business.Infrastructure.CastleWindsor.ComponentSelector;

namespace TBT.Api.Common.FluentValidation.Installers
{
    public class ValidatorsInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.Register(Component.For<IValidatorFactory>().AsFactory(c => c.SelectedWith(new FactoryComponentSelector())).LifeStyle.Transient);
        }
    }
}