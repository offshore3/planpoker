namespace Shinetech.PlanPoker.LogicModel
{
    public class InviteLogicModel
    {
        public int Id { get; set; }
        public ProjectLogicModel Project { get; set; }
        public UserLogicModel User { get; set; }
        public bool IsRegister { get; set; }
        public string InviteEmail { get; set; }
    }
}
