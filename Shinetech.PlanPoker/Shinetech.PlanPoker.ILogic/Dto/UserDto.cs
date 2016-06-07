using System.Collections.Generic;

namespace Shinetech.PlanPoker.ILogic.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public IEnumerable<ProjectDto> MyProjectsDto { get; set; }
        public IEnumerable<ProjectDto> ParticipatedProjectsDto { get; set; }
    }
}
