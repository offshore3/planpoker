using System;
using NHibernate;

namespace Shinetech.PlanPoker.Repository.UnitOfWork
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private ITransaction _transaction;

        public NHibernateUnitOfWork(ISession session)
        {
            _transaction = session.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
