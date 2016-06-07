namespace Shinetech.PlanPoker.ILogic.Dto
{
    public class InviteDto
    {
        public int Id { get; set; }
        public ProjectDto Project { get; set; }
        public UserDto User { get; set; }
        public bool IsRegister { get; set; }
    }
}
