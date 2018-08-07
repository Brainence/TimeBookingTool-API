using AutoMapper;
using Castle.Windsor;
using System;
using System.Linq;
using System.Web;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Hangfire;
using TBT.Api.Common.FluentValidation.Installers;
using TBT.Api.Common.Quartz.Jobs;
using TBT.Business.Infrastructure.CastleWindsor;
using TBT.WebApi.Common.CastleWindsor.Infrastructure;
using TBT.WebApi.Common.CastleWindsor.Installers;
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;

namespace TBT.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        private readonly IWindsorContainer _container;

        public WebApiApplication()
        {
            _container = new WindsorContainer();
            _container.Install(
                new ControllerInstaller(),
                new ComponentsInstaller(),
                new ProvidersInstaller(),
                new FactoriesInstaller(),
                new ManagerInstaller(),
                new RepositoryInstaller(),
                new ValidatorsInstaller());

            ServiceLocator.Current.SetLocatorProvider(_container);
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Services
                .Replace(typeof(IHttpControllerActivator),
                         new WindsorCompositionRoot(_container));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            InitializeAutoMapper();          
        }

        private void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("TBT")))
                {
                    foreach (var profile in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Profile))))
                    {
                        cfg.AddProfile((Profile) Activator.CreateInstance(profile, null));
                    }
                }
            });
        }

        public override void Dispose()
        {
            _container.Dispose();
            base.Dispose();
        }
    }
}
