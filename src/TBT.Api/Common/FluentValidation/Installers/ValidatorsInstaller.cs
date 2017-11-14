using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using TBT.Business.Infrastructure.CastleWindsor.ComponentSelector;
using TBT.Api.Common.FluentValidation.Store.Interfaces;
using TBT.Api.Common.FluentValidation.Store.Implementations;
using TBT.Business.Models.BusinessModels;
using TBT.Api.Common.FluentValidation.Validators;
using TBT.Api.Common.FluentValidation.Base;
using TBT.Api.Common.FluentValidation.Interfaces;

namespace TBT.Api.Common.FluentValidation.Installers
{
    public class ValidatorsInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IValidatorStore>().Named(typeof(IValidatorStore).FullName).ImplementedBy<ValidatorStore>().LifeStyle.Transient);

            container.Register(Component.For<IModelValidatorBase>().ImplementedBy<ActivityValidator>()
                .DependsOn(Property.ForKey("mode")).Named(nameof(ActivityValidator)).LifeStyle.Transient);
            container.Register(Component.For<IModelValidatorBase>().ImplementedBy<CustomerValidator>()
                .DependsOn(Property.ForKey("mode")).Named(nameof(CustomerValidator)).LifeStyle.Transient);
            container.Register(Component.For<IModelValidatorBase>().ImplementedBy<ProjectValidator>()
                .DependsOn(Property.ForKey("mode")).Named(nameof(ProjectValidator)).LifeStyle.Transient);
            container.Register(Component.For<IModelValidatorBase>().ImplementedBy<ResetTicketValidator>()
                .DependsOn(Property.ForKey("mode")).Named(nameof(ResetTicketValidator)).LifeStyle.Transient);
            container.Register(Component.For<IModelValidatorBase>().ImplementedBy<TimeEntryValidator>()
                .DependsOn(Property.ForKey("mode")).Named(nameof(TimeEntryValidator)).LifeStyle.Transient);
            container.Register(Component.For<IModelValidatorBase>().ImplementedBy<UserValidator>()
                .DependsOn(Property.ForKey("mode")).Named(nameof(UserValidator)).LifeStyle.Transient);
        }
    }
}