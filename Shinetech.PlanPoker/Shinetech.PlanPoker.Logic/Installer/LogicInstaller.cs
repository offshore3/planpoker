using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Shinetech.PlanPoker.ILogic;

namespace Shinetech.PlanPoker.Logic.Installer
{
    public class LogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserLogic>().ImplementedBy<UserLogic>().LifestylePerWebRequest());
            container.Register(Component.For<IProjectLogic>().ImplementedBy<ProjectLogic>().LifestylePerWebRequest());
            container.Register(Component.For<IInviteLogic>().ImplementedBy<InviteLogic>().LifestylePerWebRequest());
        }
    }
}
