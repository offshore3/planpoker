using System.Linq;

namespace Shinetech.PlanPoker.IRepository
{
    public interface IRepositoryBase<TEntity>
    {
        TEntity Get(object id);
        IQueryable<TEntity> Query();
        bool TryGet(object id, out TEntity entity);
        TEntity GetForUpdate(object id);
        bool TryGetForUpdate(object id, out TEntity model);
        void Save(TEntity entity);
        void Delete(object id);
    }
}