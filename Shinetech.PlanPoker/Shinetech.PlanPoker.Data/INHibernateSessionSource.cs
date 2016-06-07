using NHibernate;

namespace Shinetech.PlanPoker.Data
{
    public interface INHibernateSessionSource
    {
        ISession OpenSession();
        NHibernate.Cfg.Configuration GetConfiguration();
    }
}
