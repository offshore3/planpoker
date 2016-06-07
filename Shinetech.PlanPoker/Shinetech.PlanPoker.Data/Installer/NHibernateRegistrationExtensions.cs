using System.Collections.Generic;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NHibernate;

namespace Shinetech.PlanPoker.Data.Installer
{
    public static class NHibernateRegistrationExtensions
    {
        public static IWindsorContainer RegisterInMemoryNHibernateComponents
            (this IWindsorContainer @this,
             NHibernateRegistration registration,
             IEnumerable<Assembly> mappingAssemblies)
        {
            @this.Register(
                Component.For<INHibernateSessionSource>()
                         .UsingFactoryMethod(
                             kernel =>
                             new InMemoryNHibernateSessionSource(mappingAssemblies))
                         .Named(registration.SessionSourceName)
                         .LifestyleSingleton()
                );

            @this.Register(
                Component.For<ISession>()
                         .UsingFactoryMethod(kernel => kernel.Resolve<INHibernateSessionSource>(registration.SessionSourceName).OpenSession())
                         .Named(registration.NHibernateSessionName)
                         .LifestylePerWebRequest()
                );

            @this.Register(
                Component.For<INHibernatePersistenceSession>()
                         .ImplementedBy<NHibernatePersistenceSession>()
                         .DependsOn(registration.ISession)
                         .Named(registration.SessionName)
                         .LifestylePerWebRequest()
                );

            return @this;
        }
        public static IWindsorContainer RegisterSqlServerNHibernateComponents
            (this IWindsorContainer @this,
             NHibernateRegistration registration,
             string connectionStringName,
             IEnumerable<Assembly> mappingAssemblies)
        {
            @this.Register(
                Component.For<INHibernateSessionSource>()
                         .UsingFactoryMethod(
                             kernel =>
                             new SqlServerNHibernateSessionSource(kernel.Resolve<IConnectionStringProvider>().GetConnectionString(connectionStringName).ConnectionString, mappingAssemblies))
                         .Named(registration.SessionSourceName)
                         .LifestyleSingleton()
                );

            @this.Register(
                Component.For<ISession>()
                         .UsingFactoryMethod(kernel => kernel.Resolve<INHibernateSessionSource>(registration.SessionSourceName).OpenSession())
                         .Named(registration.NHibernateSessionName)
                         .LifestylePerWebRequest()
                );

            @this.Register(
                Component.For<INHibernatePersistenceSession>()
                         .ImplementedBy<NHibernatePersistenceSession>()
                         .DependsOn(registration.ISession)
                         .Named(registration.SessionName)
                         .LifestylePerWebRequest()
                );

            return @this;
        }
    }
}
