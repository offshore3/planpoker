using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shinetech.PlanPoker.Data.Models
{
    public class InviteModel : Entity
    {
        public virtual ProjectModel Project { get; set; }
        public virtual UserModel User { get; set; }
        public virtual bool IsRegister { get; set; }
        public InviteModel()
        {
            User=new UserModel();
            Project = new ProjectModel();
        }
    }

    
}
