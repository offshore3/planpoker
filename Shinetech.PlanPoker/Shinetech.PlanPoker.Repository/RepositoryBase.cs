using System.Linq;
using Shinetech.PlanPoker.Data;

namespace Shinetech.PlanPoker.Repository
{
    public class RepositoryBase<TEntity> where TEntity : class
    {
        private readonly INHibernatePersistenceSession _nHibernatePersistenceSession;


        public RepositoryBase(INHibernatePersistenceSession nHibernatePersistenceSession)
        {
            _nHibernatePersistenceSession = nHibernatePersistenceSession;
        }

        public TEntity Get(object id)
        {
            return _nHibernatePersistenceSession.Get<TEntity>(id);
        }

        public IQueryable<TEntity> Query()
        {
            return _nHibernatePersistenceSession.Query<TEntity>();
        }

        public bool TryGet(object id, out TEntity entity)
        {
            return _nHibernatePersistenceSession.TryGet(id, out entity);
        }

        public TEntity GetForUpdate(object id)
        {
            return _nHibernatePersistenceSession.GetForUpdate<TEntity>(id);
        }

        public bool TryGetForUpdate(object id, out TEntity entity)
        {
            return _nHibernatePersistenceSession.TryGetForUpdate(id, out entity);
        }

        public void Save(TEntity entity)
        {
            _nHibernatePersistenceSession.Save(entity);
        }

        public void Delete(object id)
        {
            _nHibernatePersistenceSession.Delete<TEntity>(id);
        }
    }
}