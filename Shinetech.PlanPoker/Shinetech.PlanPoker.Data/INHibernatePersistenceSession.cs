using System.Linq;

namespace Shinetech.PlanPoker.Data
{
    public interface INHibernatePersistenceSession
    {
        TEntity Get<TEntity>(object id);
        IQueryable<TEntity> Query<TEntity>();
        bool TryGet<TEntity>(object id, out TEntity entity);
        TEntity GetForUpdate<TEntity>(object id);
        bool TryGetForUpdate<TEntity>(object id, out TEntity model);
        void Save<TEntity>(TEntity entity);
        void Delete<TEntity>(object id);
    }
}