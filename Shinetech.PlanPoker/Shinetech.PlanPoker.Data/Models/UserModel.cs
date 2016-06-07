using System.Collections.Generic;

namespace Shinetech.PlanPoker.Data.Models
{
    public class UserModel : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string Image { get; set; }
        public virtual IEnumerable<ProjectModel> MyProjects { get; set; }
        public virtual IEnumerable<ProjectModel> ParticipatedProjects { get; set; }
    }

    public abstract class Entity
    {
        public virtual int Id { get; set; }
    }
}
