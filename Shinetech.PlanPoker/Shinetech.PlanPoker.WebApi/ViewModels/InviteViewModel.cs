namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public class InviteViewModel
    {
        public int Id { get; set; }
        public ProjectViewModel Project { get; set; }
        public UserViewModel User { get; set; }
        public bool IsRegister { get; set; }
        public string InviteEmail { get; set; }
    }
}