using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Shinetech.PlanPoker.Repository.Installer;

namespace Shinetech.PlanPoker.WebApi.Installer
{
    public static class WindsorBootstrapper
    {
        private static IWindsorContainer _container;

        public static void Initialize()
        {
            _container = new WindsorContainer();
            _container.Register(Component.For<IWindsorContainer>().Instance(_container).LifestyleSingleton());
            _container.Install(FromAssembly.This(),FromAssembly.Containing<RepositoryInstaller>()
                
                );

        }
        public static IWindsorContainer Container => _container;
    }
}