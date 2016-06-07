using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Shinetech.PlanPoker.Data;
using Shinetech.PlanPoker.Data.Installer;
using Shinetech.PlanPoker.Data.Maps;
using Shinetech.PlanPoker.IRepository;
using Shinetech.PlanPoker.Repository.UnitOfWork;

namespace Shinetech.PlanPoker.Repository.Installer
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public class ComponentKeys
        {
            public const string ConnectionStringName = "PlanPoker";
            public const string RelationalQueryModelSession = "PlanPokerSession";
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            CommonInstaller(container);
        }

        private static void CommonInstaller(IWindsorContainer container)
        {

            container.Register(Component.For<IConnectionStringProvider>().ImplementedBy<ConnectionStringProvider>().LifestylePerWebRequest());

            container.RegisterSqlServerNHibernateComponents(new NHibernateRegistration<RepositoryInstaller>(),
                  ComponentKeys.ConnectionStringName,
                  new[] { typeof(UserModelMap).Assembly, typeof(ProjectModelMap).Assembly, typeof(InviteModelMap).Assembly, }
                  );
            Component.For<IConnectionStringProvider>()
                .ImplementedBy<ConnectionStringProvider>()
                .DependsOn(new Dependency[] { Dependency.OnValue(typeof(string), ComponentKeys.ConnectionStringName) })
                .LifestylePerWebRequest();

            container.Register(Component.For<IUnitOfWorkFactory>().ImplementedBy<UnitOfWorkFactory>().LifestylePerWebRequest());
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<NHibernateUnitOfWork>().LifestylePerWebRequest());
            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestylePerWebRequest());
        }
        public static void InstallForTest(IWindsorContainer container)
        {

            container.Kernel.ComponentModelBuilder.AddContributor(
                new LifestyleRegistrationMutator(originalLifestyle: LifestyleType.PerWebRequest, newLifestyleType: LifestyleType.Scoped)
                );
            CommonInstaller(container);
        }
        public class LifestyleRegistrationMutator : IContributeComponentModelConstruction
        {
            private readonly LifestyleType _originalLifestyle;
            private readonly LifestyleType _newLifestyleType;

            public LifestyleRegistrationMutator(
                LifestyleType originalLifestyle = LifestyleType.PerWebRequest,
                LifestyleType newLifestyleType = LifestyleType.Scoped)
            {
                _originalLifestyle = originalLifestyle;
                _newLifestyleType = newLifestyleType;
            }

            public void ProcessModel(IKernel kernel,
                                     ComponentModel model)
            {
                if (model.LifestyleType == _originalLifestyle)
                    model.LifestyleType = _newLifestyleType;
            }
        }
    }
}
