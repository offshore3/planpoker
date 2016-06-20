using Shinetech.PlanPoker.Data;
using Shinetech.PlanPoker.Data.Models;
using Shinetech.PlanPoker.IRepository;

namespace Shinetech.PlanPoker.Repository
{
    public class InviteRepository : RepositoryBase<InviteModel>, IInviteRepository
    {
        private readonly INHibernatePersistenceSession _nHibernatePersistenceSession;
        public InviteRepository(INHibernatePersistenceSession nHibernatePersistenceSession) : base(nHibernatePersistenceSession)
        {
            _nHibernatePersistenceSession = nHibernatePersistenceSession;
        }

        public void DeleteByProjectId(int projectId)
        {
            var session = _nHibernatePersistenceSession.GetSession();
            var sql = $"Delete from InviteModel Where ProjectId = {projectId}";
            session.CreateSQLQuery(sql).ExecuteUpdate();
        }
    }
}
