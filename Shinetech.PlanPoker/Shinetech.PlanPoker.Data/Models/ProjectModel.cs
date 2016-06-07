using System.Collections.Generic;

namespace Shinetech.PlanPoker.Data.Models
{
    public class ProjectModel : Entity
    {
        public virtual string Name { get; set; }
        public virtual UserModel Owner { get; set; }
        public virtual IEnumerable<UserModel> Participates { get; set; }
    }
}
