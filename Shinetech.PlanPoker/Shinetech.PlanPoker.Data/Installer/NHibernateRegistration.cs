using Castle.MicroKernel.Registration;
using NHibernate;

namespace Shinetech.PlanPoker.Data.Installer
{
    public class NHibernateRegistration
    {
        protected NHibernateRegistration(string uniqueNameOfRegistration)
        {
            SessionSourceName = $"{uniqueNameOfRegistration}.SessionSource";
            NHibernateSessionName = $"{uniqueNameOfRegistration}.NHibernateSession";
            SessionName = $"{uniqueNameOfRegistration}.Session";
        }

        // ReSharper disable InconsistentNaming
        public Dependency NHibernatePersistenceSession => Dependency.OnComponent(typeof(INHibernatePersistenceSession), componentName: SessionName);
        public Dependency ISession => Dependency.OnComponent(typeof(ISession), componentName: NHibernateSessionName);
        // ReSharper restore InconsistentNaming

        internal string SessionSourceName { get; }
        internal string NHibernateSessionName { get; }
        internal string SessionName { get; }
    }

    public class NHibernateRegistration<TClient> : NHibernateRegistration
    {
        public NHibernateRegistration() : base(typeof(TClient).FullName) { }
    }
}