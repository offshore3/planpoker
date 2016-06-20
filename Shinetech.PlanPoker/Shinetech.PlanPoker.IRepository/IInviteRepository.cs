using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.IRepository
{
    public interface IInviteRepository : IRepositoryBase<InviteModel>
    {
        void DeleteByProjectId(int projectId);
    }
}