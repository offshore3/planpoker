namespace Shinetech.PlanPoker.Data.Models
{
    public class InviteModel : Entity
    {
        public virtual ProjectModel Project { get; set; }
        public virtual UserModel User { get; set; }
        public virtual bool IsRegister { get; set; }
    }
}
