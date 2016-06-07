using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Shinetech.PlanPoker.Logic.Installer;
using Shinetech.PlanPoker.Repository.Installer;

namespace Shinetech.PlanPoker.WebApi.Installer
{
    public static class WindsorBootstrapper
    {
        public static void Initialize(IWindsorContainer container)
        {
            Container = container;
            InitializeCommon(container);
        }

        public static void InitializeCommon(IWindsorContainer container)
        {
            container.Install(FromAssembly.This(),
                FromAssembly.Containing<RepositoryInstaller>(),
                FromAssembly.Containing<LogicInstaller>()
                );

            container.Register(Component.For<IWindsorContainer>().Instance(container).LifestyleSingleton());
        }
        public static IWindsorContainer Container { get; private set; }
    }
}