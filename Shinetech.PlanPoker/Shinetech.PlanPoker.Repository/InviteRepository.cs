using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shinetech.PlanPoker.Data;
using Shinetech.PlanPoker.Data.Models;
using Shinetech.PlanPoker.IRepository;

namespace Shinetech.PlanPoker.Repository
{
    public class InviteRepository : RepositoryBase<InviteModel>, IInviteRepository
    {
        public InviteRepository(INHibernatePersistenceSession nHibernatePersistenceSession) : base(nHibernatePersistenceSession)
        {
        }
    }
}
