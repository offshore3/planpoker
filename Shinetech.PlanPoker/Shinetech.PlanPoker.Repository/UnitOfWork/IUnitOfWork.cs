using System;

namespace Shinetech.PlanPoker.Repository.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        void Commit();
    }
}
