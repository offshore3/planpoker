using Shinetech.PlanPoker.Data;
using Shinetech.PlanPoker.Data.Models;
using Shinetech.PlanPoker.IRepository;

namespace Shinetech.PlanPoker.Repository
{
    public class ProjectRepository : RepositoryBase<ProjectModel>, IProjectRepository
    {
        public ProjectRepository(INHibernatePersistenceSession nHibernatePersistenceSession) : base(nHibernatePersistenceSession)
        {
        }
    }
}
