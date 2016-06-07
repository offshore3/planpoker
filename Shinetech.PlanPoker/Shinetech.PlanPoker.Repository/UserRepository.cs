using Shinetech.PlanPoker.Data;
using Shinetech.PlanPoker.Data.Models;
using Shinetech.PlanPoker.IRepository;

namespace Shinetech.PlanPoker.Repository
{
    public class UserRepository : RepositoryBase<UserModel>, IUserRepository
    {
        public UserRepository(INHibernatePersistenceSession nHibernatePersistenceSession) : base(nHibernatePersistenceSession)
        {
        }
    }
}
