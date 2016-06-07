using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shinetech.PlanPoker.Data.Models
{
    public class ProjectModel : Entity
    {
        public virtual string Name { get; set; }
        public virtual UserModel Owner { get; set; }
        public virtual IList<UserModel> Participates { get; set; }
    }
}
