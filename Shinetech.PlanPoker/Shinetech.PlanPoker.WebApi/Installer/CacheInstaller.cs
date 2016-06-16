using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Shinetech.PlanPoker.Data.Common;

namespace Shinetech.PlanPoker.WebApi.Installer
{
    public class CacheInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICacheManager>().ImplementedBy<MemoryCacheManager>().LifestyleSingleton()
                );
        }
    }
}